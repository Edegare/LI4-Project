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

        // Métodos SubMateriais
        public async Task<List<Material>> GetMateriais()
        {
            Task<List<Material>> materiais = _context.Material.ToListAsync();
            return await materiais;
//            return await _context.Material.ToListAsync();
        }

        public async Task PutMaterial(Material material)
        {
            await _context.Material.AddAsync(material);
            await _context.SaveChangesAsync();
        }
    }
}
