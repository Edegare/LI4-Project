using Microsoft.EntityFrameworkCore;
using BMManager.BMManagerCD;
using BMManagerLN.SubMateriais;
using BMManagerLN.SubFuncionarios;
using Azure;

namespace BMManagerLN.SubMoveis
{
    public class CSubMoveis : APICSubMoveis
    {
        private readonly BMManagerContext _context;

        public CSubMoveis(BMManagerContext context)
        {
            _context = context;
        }

        // MÈtodos SubMoveis
        public async Task<List<Movel>> GetMoveis()
        {
            return await _context.Movel.ToListAsync();
        }

        public async Task PutMovel(Movel movel)
        {
<<<<<<< HEAD
            return;
=======
            await _context.Movel.AddAsync(movel);
            await _context.SaveChangesAsync();
>>>>>>> 01d059c (Registar m√≥vel)
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
