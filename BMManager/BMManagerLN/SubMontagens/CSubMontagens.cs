using Microsoft.EntityFrameworkCore;
using BMManager.BMManagerCD;
using BMManagerLN.SubMateriais;
using BMManagerLN.SubMoveis;

namespace BMManagerLN.SubMontagens
{
    public class CSubMontagens : APICSubMontagens
    {
        private readonly BMManagerContext _context;
        public int OrdenarEstado(Estado estado)
        {
            switch (estado)
            {
                case Estado.Em_Pausa: return 1;
                case Estado.Em_Progresso: return 2;
                case Estado.Concluida: return 3;
                case Estado.Cancelada: return 4;
                default: return 0;
            }
        }
        public CSubMontagens(BMManagerContext context)
        {
            _context = context;
        }

        // M�todos SubMontagens
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
        public async Task AssociarAEncomenda(int codMontagem, int codEncomenda)
        {
            Montagem montagem = await _context.Montagem.FindAsync(codMontagem);
            if (montagem != null)
            {
                montagem.Encomenda = codEncomenda;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Montagem n�o encontrada.");
            }
        }
    }
}
