using Microsoft.EntityFrameworkCore;
using BMManager.BMManagerCD;
using BMManagerLN.SubMoveis;
using BMManagerLN.SubMateriais;


namespace BMManagerLN.SubEncomendas
{
    public class CSubEncomendas : APICSubEncomendas
    {
        private readonly BMManagerContext _context;

        public CSubEncomendas(BMManagerContext context)
        {
            _context = context;
        }

        // Métodos SubEncomendas
        public async Task<List<Encomenda>> GetEncomendas()
        {
            return await _context.Encomenda.ToListAsync();
        }

        public async Task<Encomenda> GetEncomenda(int codEncomenda)
        {
            return await _context.Encomenda
                                 .FirstOrDefaultAsync(f => f.Numero == codEncomenda
                                 );
        }

        public async Task PutEncomenda(Encomenda encomenda)
        {
            await _context.Encomenda.AddAsync(encomenda);
            await _context.SaveChangesAsync();
        }
        public async Task ConcluirEncomenda(int codEncomenda)
        {
            Encomenda encomenda = await _context.Encomenda.FindAsync(codEncomenda);
            encomenda.Concluida = true;
            encomenda.Data_Real = DateOnly.FromDateTime(DateTime.Today);
            await _context.SaveChangesAsync();
        }
    }
}
