using Microsoft.EntityFrameworkCore;
using BMManager.BMManagerCD;

namespace BMManagerLN.SubMateriais
{
    public class CSubMateriais : APICSubMateriais
    {
        private readonly BMManagerContext _context;

        public CSubMateriais(BMManagerContext context)
        {
            _context = context;
        }

        // M�todos SubMateriais
        public async Task<List<Material>> GetMateriais()
        {
            return await _context.Material.ToListAsync();
        }

        public async Task<Material> GetMaterial(int codMaterial)
        {
            return await _context.Material.FindAsync(codMaterial);
        }

        public async Task PutMaterial(Material material)
        {
            await _context.Material.AddAsync(material);
            await _context.SaveChangesAsync();
        }

        public async Task<Dictionary<Material, int>> GetMateriaisEtapa(int codEtapa)
        {
            List<Etapa_Precisa_Material> etapasMateriaisQuantidades = await _context.Etapa_Precisa_Material.Where(epm => epm.Etapa == codEtapa).ToListAsync();
            return await MateriaisQuantidades(etapasMateriaisQuantidades);
        }

        public async Task<Dictionary<Material, int>> GetMateriaisEtapas(int[] codEtapas)
        {
            List<Etapa_Precisa_Material> etapasMateriaisQuantidades = await _context.Etapa_Precisa_Material.Where(epm => codEtapas.Contains(epm.Etapa)).ToListAsync();
            return await MateriaisQuantidades(etapasMateriaisQuantidades);
        }

        private async Task<Dictionary<Material, int>> MateriaisQuantidades(List<Etapa_Precisa_Material> etapasMateriaisQuantidades)
        {
            Dictionary<int, Material> materiaisExistentes = new Dictionary<int, Material>();
            Dictionary<Material, int> materiais = new Dictionary<Material, int>();
            foreach (Etapa_Precisa_Material epm in etapasMateriaisQuantidades)
            {
                Material material;
                if (!materiaisExistentes.ContainsKey(epm.Material))
                {
                    material = await GetMaterial(epm.Material);
                    materiaisExistentes[epm.Material] = material;
                    materiais[material] = 0;
                }
                else
                {
                    material = materiaisExistentes[epm.Material];
                }
                materiais[material] += epm.Quantidade;
            }
            return materiais;
        }

        public async Task AdicionaMaterialEtapa(int codMaterial, int quantidade, int codEtapa)
        {
            bool associado = _context.Etapa_Precisa_Material.Any(e => e.Material == codMaterial & e.Etapa == codEtapa);
            if (!associado)
            {
                Etapa_Precisa_Material epm = new Etapa_Precisa_Material()
                {
                    Etapa = codEtapa,
                    Material = codMaterial,
                    Quantidade = quantidade
                };
                await _context.Etapa_Precisa_Material.AddAsync(epm);
                await _context.SaveChangesAsync();
            }
            else
            {
                Etapa_Precisa_Material epm = await _context.Etapa_Precisa_Material.FindAsync(codEtapa, codMaterial);
                epm.Quantidade += quantidade;
                await _context.SaveChangesAsync();
            }
        }

        public async Task AlterarQuantidadeMaterial(int codMaterial, int novaQuantidade)
        {
            Material mat = await _context.Material.FindAsync(codMaterial);
            if (mat != null)
            {
                mat.Quantidade = novaQuantidade;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Material não encontrado.");
            }
        }

        public async Task AtualizaStockMateriaisEtapa(int codEtapa)
        {
            Dictionary<int, int> materiaisQuantidades = await _context.Etapa_Precisa_Material.Where(e => e.Etapa == codEtapa).ToDictionaryAsync(e => e.Material, e => e.Quantidade);
            foreach (KeyValuePair<int, int> materialQuantidade in materiaisQuantidades)
            {
                Material material = await GetMaterial(materialQuantidade.Key);
                material.Quantidade -= materialQuantidade.Value;
                await _context.SaveChangesAsync();
            }
        }
    }
}
