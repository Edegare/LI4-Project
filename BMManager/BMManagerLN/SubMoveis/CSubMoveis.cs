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

        // Métodos SubMoveis
        public async Task<List<Movel>> GetMoveis()
        {
            return await _context.Movel.ToListAsync();
        }

        public async Task<Movel> GetMovel(int codMovel)
        {
            return await _context.Movel.FindAsync(codMovel);
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

        public async Task<Etapa> GetEtapa(int codEtapa)
        {
            return await _context.Etapa
                                 .FirstOrDefaultAsync(f => f.Codigo_Etapa == codEtapa);
        }

        public async Task<Dictionary<int,Etapa>> GetEtapasMovel(int codMovel)
        {
            return await _context.Etapa.Where(e => e.Movel == codMovel).ToDictionaryAsync(e => e.Numero);
        }

        public async Task<Dictionary<int, Etapa>> GetEtapasMovelCondicao(Func<Etapa,bool> condicao)
        {
            return _context.Etapa.AsEnumerable().Where(e => condicao(e)).ToDictionary(e => e.Codigo_Etapa);
        }

        public async Task PutEtapa(Etapa etapa)
        {
            await _context.Etapa.AddAsync(etapa);
            await _context.SaveChangesAsync();
        }
    }
}
