using BMManager;

namespace BMManagerLN.SubFuncionarios
{
    public interface APICSubFuncionarios
    {
        Task<List<Funcionario>> GetFuncionarios();
        Task<Funcionario> GetFuncionario(int codFuncionario);
        Task PutFuncionario(Funcionario funcionario);
        Task AssociaFuncionarioMontagem(int codFuncionario, int codMontagem);
        Task<FuncionarioDTO?> AutenticarUtilizador(string codigo, string senha);
        Task<List<Funcionario>> FuncionariosParticipamMontagem(int codMontagem);
    }
}
