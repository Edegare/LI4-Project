using Microsoft.EntityFrameworkCore;
using BMManager.BMManagerCD;

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

        public async Task PutMontagem(Montagem montagem)
        {
            await _context.Montagem.AddAsync(montagem);
            await _context.SaveChangesAsync();
        }
    }
}
