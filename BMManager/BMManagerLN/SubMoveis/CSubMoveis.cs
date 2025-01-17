using Microsoft.EntityFrameworkCore;
using BMManager.BMManagerCD;
using BMManagerLN.SubMateriais;
using BMManagerLN.SubFuncionarios;

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
            return null;
        }

        public async Task PutMovel(Movel movel)
        {
            return;
        }
        public async Task<List<Etapa>> GetEtapas()
        {
            return await _context.Etapa.ToListAsync();
        }

        public async Task<Etapa> GetEtapa(int codEtapa)
        {
            return await _context.Etapa
                                 .FirstOrDefaultAsync(f => f.Codigo_Etapa == codEtapa);
        }


        public async Task PutEtapa(Etapa etapa)
        {
            await _context.Etapa.AddAsync(etapa);
            await _context.SaveChangesAsync();
        }
    }
}
