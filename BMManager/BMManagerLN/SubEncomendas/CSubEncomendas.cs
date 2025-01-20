using Microsoft.EntityFrameworkCore;
using BMManager.BMManagerCD;


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
            }
            else
            {
                Encomenda_Precisa_Movel epm = await _context.Encomenda_Precisa_Movel.FindAsync(codEncomenda, codMovel);
                epm.Quantidade += quantidade;
                await _context.SaveChangesAsync();
            }
        }
    }
}
