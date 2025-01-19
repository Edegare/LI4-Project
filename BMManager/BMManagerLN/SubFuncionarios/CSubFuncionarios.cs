using System.Linq;
using Microsoft.EntityFrameworkCore;
using BMManager.BMManagerCD;
using BMManager;

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
                                 .FindAsync(codFuncionario);
        }

        public async Task PutFuncionario(Funcionario funcionario)
        {
            await _context.Funcionario.AddAsync(funcionario);
            await _context.SaveChangesAsync();
        }

        public async Task<FuncionarioDTO?> AutenticarUtilizador(string codigo, string senha)
        {
            if (int.TryParse(codigo, out int codFuncionario))
            {
                var funcionario = await _context.Funcionario.FindAsync(codFuncionario);

                if (funcionario != null && funcionario.Senha == senha)
                {
                    return new FuncionarioDTO(funcionario.Codigo_Utilizador, funcionario.Nome, funcionario.Equipa.ToString());
                }
            }
            return null;
        }

        public async Task<List<Funcionario>> FuncionariosParticipamMontagem(int codMontagem)
        {
            List<Funcionario_Participa_Montagem> funcionariosParticipamMontagem = await _context.Funcionario_Participa_Montagem.Where(f => f.Montagem == codMontagem).ToListAsync();
            List<Funcionario> funcionarios = new List<Funcionario>();
            foreach (Funcionario_Participa_Montagem fpm in funcionariosParticipamMontagem)
            {
                Funcionario funcionario = await GetFuncionario(fpm.Funcionario);
                funcionarios.Add(funcionario);
            }
            return funcionarios;
        }
    }
}
