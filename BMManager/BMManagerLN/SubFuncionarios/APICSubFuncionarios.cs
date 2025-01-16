
namespace BMManagerLN.SubFuncionarios
{
    public interface APICSubFuncionarios
    {
        Task<List<Funcionario>> GetFuncionarios();
        Task<Funcionario> GetFuncionario(int codFuncionario);
        Task PutFuncionario(Funcionario funcionario);
        Task<bool> AutenticarUtilizador(string codigo, string senha);
    }
}
