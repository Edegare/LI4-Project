using Microsoft.EntityFrameworkCore;
using BMManager.BMManagerCD;

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
                throw new Exception("Montagem não encontrada.");
            }
        }

        public async Task AtualizaMontagem(Montagem montagemAtualizada)
        {
            Montagem montagem = await _context.Montagem.FindAsync(montagemAtualizada.Numero);
            if (montagem != null)
            {
                montagem.Data_Final = montagemAtualizada.Data_Final;
                montagem.Duracao = montagemAtualizada.Duracao;
                montagem.Estado = montagemAtualizada.Estado;
                montagem.Etapa_Concluida = montagemAtualizada.Etapa_Concluida;
                montagem.Etapa = montagemAtualizada.Etapa;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Montagem não encontrada.");
            }
        }
    }
}
