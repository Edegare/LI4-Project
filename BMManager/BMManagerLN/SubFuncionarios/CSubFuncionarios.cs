using System.Linq;
using Microsoft.EntityFrameworkCore;
using BMManager.BMManagerCD;

namespace BMManagerLN.SubFuncionarios
{
    public class CSubFuncionarios : APICSubFuncionarios
    {
        private readonly BMManagerContext _context;

        public CSubFuncionarios(BMManagerContext context)
        {
            _context = context;
        }

        // Métodos SubFuncionários
        public async Task<List<Funcionario>> GetFuncionarios()
        {
            return await _context.Funcionario.ToListAsync();
        }

        public async Task<Funcionario> GetFuncionario(int codFuncionario)
        {
            return await _context.Funcionario
                                 .FirstOrDefaultAsync(f => f.Codigo_Utilizador == codFuncionario);
        }

        public async Task PutFuncionario(Funcionario funcionario)
        {
            await _context.Funcionario.AddAsync(funcionario);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AutenticarUtilizador(string codigo, string senha)
        {
            if (int.TryParse(codigo, out int codFuncionario))
            {
                var funcionario = await _context.Funcionario
                                                .FirstOrDefaultAsync(f => f.Codigo_Utilizador == codFuncionario);

                if (funcionario != null && funcionario.Senha == senha)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
