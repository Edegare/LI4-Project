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

        // M�todos SubFuncion�rios
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

        public async Task ModificarFuncionario (int codFuncionario, string nome, string email, string senha, string telefone, Equipa? equipa, bool conta_ativa) {
            Funcionario fun = await _context.Funcionario.FindAsync(codFuncionario);
            if (fun != null)
            {
                fun.Nome = nome;
                fun.Email = email;
                fun.Senha = senha;
                fun.Telefone = telefone;
                fun.Equipa = equipa;
                fun.Conta_Ativa = conta_ativa;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Funcionário não encontrado.");
            }
        }

        public async Task AssociaFuncionarioMontagem(int codFuncionario, int codMontagem)
        {
            bool associado = _context.Funcionario_Participa_Montagem.Any(f => f.Montagem == codMontagem & f.Funcionario == codFuncionario);
            if (!associado)
            {
                Funcionario_Participa_Montagem fpm = new Funcionario_Participa_Montagem()
                    {
                        Montagem = codMontagem,
                        Funcionario = codFuncionario
                    };
                await _context.Funcionario_Participa_Montagem.AddAsync(fpm);
            }
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
