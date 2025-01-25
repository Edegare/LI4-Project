using Microsoft.EntityFrameworkCore;
using BMManager.BMManagerCD;
using BMManagerLN.SubMateriais;

namespace BMManagerLN.SubMoveis
{
    public class CSubMoveis : APICSubMoveis
    {
        private readonly BMManagerContext _context;

        public CSubMoveis(BMManagerContext context)
        {
            _context = context;
        }

        // M�todos SubMoveis
        public async Task<List<Movel>> GetMoveis()
        {
            return await _context.Movel.ToListAsync();
        }

        public async Task<List<Movel>> GetMoveisSemImagens()
        {
            return await _context.Movel.Select(m => new Movel
                                            {
                                                Numero = m.Numero,
                                                Nome = m.Nome,
                                                Quantidade = m.Quantidade
                                            }).ToListAsync();
        }

        public async Task<List<Encomenda_Precisa_Movel>> GetEncomendaPrecisaMovel() {
            return await _context.Encomenda_Precisa_Movel.ToListAsync();
        }

        public async Task<Movel> GetMovel(int codMovel)
        {
            return await _context.Movel.FindAsync(codMovel);
        }

        public async Task<Movel> GetMovelSemImagem(int codMovel)
        {
            return await _context.Movel.Where(m => m.Numero == codMovel).Select(m => new Movel
                                                                                    {
                                                                                        Numero = m.Numero,
                                                                                        Nome = m.Nome,
                                                                                        Quantidade = m.Quantidade
                                                                                    }).FirstOrDefaultAsync() ?? new Movel();
        }

        public bool MovelExiste(int codMovel)
        {
            return _context.Movel.Any(m => m.Numero == codMovel);
        }

        public async Task PutMovel(Movel movel)
        {
            await _context.Movel.AddAsync(movel);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Etapa>> GetEtapas()
        {
            return await _context.Etapa.ToListAsync();
        }

        public async Task<List<Etapa>> GetEtapasSemImagens()
        {
            return await _context.Etapa.Select(e => new Etapa
                                            {
                                                Codigo_Etapa = e.Codigo_Etapa,
                                                Numero = e.Numero,
                                                Proxima_Etapa = e.Proxima_Etapa,
                                                Movel = e.Movel
                                            }).ToListAsync();
        }

        public async Task<Etapa> GetEtapa(int codEtapa)
        {
            return await _context.Etapa
                                 .FirstOrDefaultAsync(f => f.Codigo_Etapa == codEtapa) ?? new Etapa();
        }

        public async Task<Etapa> GetEtapaSemImagem(int codEtapa)
        {
            return await _context.Etapa.Where(e => e.Codigo_Etapa == codEtapa).Select(e => new Etapa
                                                                                    {
                                                                                        Codigo_Etapa = e.Codigo_Etapa,
                                                                                        Numero = e.Numero,
                                                                                        Proxima_Etapa = e.Proxima_Etapa,
                                                                                        Movel = e.Movel
                                                                                    }).FirstOrDefaultAsync() ?? new Etapa();
        }

        public async Task<Dictionary<int,Etapa>> GetEtapasMovel(int codMovel)
        {
            return await _context.Etapa.Where(e => e.Movel == codMovel).ToDictionaryAsync(e => e.Numero);
        }

        public async Task<Dictionary<int, Etapa>> GetEtapasMovelSemImagens(int codMovel)
        {
            return await _context.Etapa.Where(e => e.Movel == codMovel).Select(e => new Etapa
                                                                            {
                                                                                Codigo_Etapa = e.Codigo_Etapa,
                                                                                Numero = e.Numero,
                                                                                Proxima_Etapa = e.Proxima_Etapa,
                                                                                Movel = e.Movel
                                                                            }).ToDictionaryAsync(e => e.Numero);
        }

        public async Task<Dictionary<int, Etapa>> GetEtapasMovelCondicao(Func<Etapa,bool> condicao)
        {
            Dictionary<int,Etapa> etapas = _context.Etapa.AsEnumerable().Where(e => condicao(e)).ToDictionary(e => e.Numero);
            return etapas;
        }

        public async Task<Dictionary<int, Etapa>> GetEtapasMovelCondicaoSemImagem(Func<Etapa, bool> condicao)
        {
            Dictionary<int, Etapa> etapas = _context.Etapa.AsEnumerable().Where(e => condicao(e)).Select(e => new Etapa
                                                                                                    {
                                                                                                        Codigo_Etapa = e.Codigo_Etapa,
                                                                                                        Numero = e.Numero,
                                                                                                        Proxima_Etapa = e.Proxima_Etapa,
                                                                                                        Movel = e.Movel
                                                                                                    }).ToDictionary(e => e.Numero);
            return etapas;
        }

        public async Task PutEtapa(Etapa etapa)
        {
            await _context.Etapa.AddAsync(etapa);
            await _context.SaveChangesAsync();
        }

        public async Task<Dictionary<Movel,int>> GetMoveisEncomenda(int codEncomenda)
        {
            Dictionary<int, int> codMoveisQuantidade = await _context.Encomenda_Precisa_Movel
                                                                .Where(e => e.Encomenda == codEncomenda).ToDictionaryAsync(e => e.Movel, e => e.Quantidade);
            Dictionary<Movel,int> moveisQuantidade = new Dictionary<Movel,int>();
            foreach (KeyValuePair<int, int> movelQuantidade in codMoveisQuantidade)
            {
                Movel movel = await _context.Movel.FindAsync(movelQuantidade.Key);
                moveisQuantidade.Add(movel, movelQuantidade.Value);
            }
            return moveisQuantidade;
        }

        public async Task AdicionaMovelEncomenda(int codMovel, int quantidade, int codEncomenda)
        {
            bool associado = _context.Encomenda_Precisa_Movel.Any(e => e.Movel == codMovel & e.Encomenda == codEncomenda);
            if (!associado)
            {
                Encomenda_Precisa_Movel epm = new Encomenda_Precisa_Movel()
                {
                    Encomenda = codEncomenda,
                    Movel = codMovel,
                    Quantidade = quantidade
                };
                await _context.Encomenda_Precisa_Movel.AddAsync(epm);
                await _context.SaveChangesAsync();
            }
            else
            {
                Encomenda_Precisa_Movel epm = await _context.Encomenda_Precisa_Movel.FindAsync(codEncomenda, codMovel);
                epm.Quantidade += quantidade;
                await _context.SaveChangesAsync();
            }
        }

        public async Task IncrementarQuantidadeMovel(int codMovel) {

            var movel = await _context.Movel
                               .FirstOrDefaultAsync(m => m.Numero == codMovel);

            if (movel == null)
            {
                throw new ArgumentException("Móvel não encontrado");
            }

            movel.Quantidade++;

            await _context.SaveChangesAsync();
        }
        public async Task DiminuirQuantidadeMovel(int codMovel)
        {

            var movel = await _context.Movel
                               .FirstOrDefaultAsync(m => m.Numero == codMovel);

            if (movel == null)
            {
                throw new ArgumentException("Móvel não encontrado");
            }

            movel.Quantidade--;

            await _context.SaveChangesAsync();
        }
    }
}
