using Microsoft.EntityFrameworkCore;
using BMManager.BMManagerCD;
using BMManagerLN.SubMateriais;
using BMManagerLN.SubMoveis;

namespace BMManagerLN.SubMontagens
{
    public class CSubMontagens : APICSubMontagens
    {
        private readonly BMManagerContext _context;

        public CSubMontagens(BMManagerContext context)
        {
            _context = context;
        }

        // Métodos SubMontagens
        public async Task<List<Montagem>> GetMontagens()
        {
            return await _context.Montagem.ToListAsync();
        }

        public async Task<Montagem> GetMontagem(int codMontagem)
        {
            return await _context.Montagem.FindAsync(codMontagem);
        }

        public async Task PutMontagem(Montagem montagem)
        {
            await _context.Montagem.AddAsync(montagem);
            await _context.SaveChangesAsync();
        }
    }
}
