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

        //Métodos SubMontagens
        Task<List<Montagem>> GetMontagens();
        Task NovaMontagem(int codMovel, int codFuncionario);


        //Métodos SubMoveis
        Task<List<Movel>> GetMoveis();
        Task PutMovel(Movel movel);
        Task<List<Etapa>> GetEtapas();
        Task<Etapa> GetEtapa(int codEtapa);
        Task PutEtapa(Etapa etapa);


        //Métodos SubMateriais
        Task<List<Material>> GetMateriais();
        Task<Material> GetMaterial(int codMaterial);
        Task PutMaterial(Material material);


        //Métodos SubEncomendas
        Task<List<Encomenda>> GetEncomendas();
        Task<Encomenda> GetEncomenda(int codEncomenda);
        Task PutEncomenda(Encomenda encomenda);
    }
}