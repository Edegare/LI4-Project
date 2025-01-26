using BMManager.Utils;

namespace BMManagerLN.SubFuncionarios
{
    public interface APICSubFuncionarios
    {
        Task<List<Funcionario>> GetFuncionarios();
        Task<Funcionario> GetFuncionario(int codFuncionario);
        Task PutFuncionario(Funcionario funcionario);
        Task ModificarFuncionario (int codFuncionario, string nome, string email, string senha, string telefone, Equipa? equipa, bool conta_ativa);
        Task AssociaFuncionarioMontagem(int codFuncionario, int codMontagem);
        Task<FuncionarioDTO?> AutenticarUtilizador(string codigo, string senha);
        Task<List<Funcionario>> FuncionariosParticipamMontagem(int codMontagem);

        Task<bool> AtualizarFuncionario(Funcionario funcionario);
    }
}
