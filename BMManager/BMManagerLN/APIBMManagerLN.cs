using BMManager;
using BMManagerLN.SubEncomendas;
using BMManagerLN.SubFuncionarios;
using BMManagerLN.SubMateriais;
using BMManagerLN.SubMontagens;
using BMManagerLN.SubMoveis;

namespace BMManagerLN
{
    public interface APIBMManagerLN
    {
        //Métodos SubFuncionários
        Task<List<Funcionario>> GetFuncionarios();
        Task<Funcionario> GetFuncionario(int codFuncionario);
        Task PutFuncionario(Funcionario funcionario);
        Task<FuncionarioDTO?> AutenticarUtilizador(string codigo, string senha);
        Task<List<Funcionario>> FuncionariosParticipamMontagem(int codMontagem);

        //Métodos SubMontagens
        Task<List<Montagem>> GetMontagens();
        Task<Montagem> GetMontagem(int codMontagem);
        Task NovaMontagem(int codMovel, int codFuncionario);
        Task<Dictionary<Material, int>> MateriaisUtilizadosMontagem(int codMontagem);
        Task<Dictionary<Material, int>> MateriaisPorUtilizarMontagem(int codMontagem);


        //Métodos SubMoveis
        Task<List<Movel>> GetMoveis();
        Task<Movel> GetMovel(int codMovel);
        Task PutMovel(Movel movel);
        Task<List<Etapa>> GetEtapas();
        Task<Etapa> GetEtapa(int codEtapa);
        Task<Dictionary<int,Etapa>> GetEtapasMovel(int codMovel);
        Task PutEtapa(Etapa etapa);


        //Métodos SubMateriais
        Task<List<Material>> GetMateriais();
        Task<Material> GetMaterial(int codMaterial);
        Task PutMaterial(Material material);


        //Métodos SubEncomendas
        Task<List<Encomenda>> GetEncomendas();
        Task<Encomenda> GetEncomenda(int codEncomenda);
        Task PutEncomenda(Encomenda encomenda);

        //Métodos Auxiliares
        string GetDescricao(Enum valor);
    }
}