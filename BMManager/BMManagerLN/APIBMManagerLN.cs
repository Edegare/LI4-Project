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
        Task PutFuncionario(Funcionario funcionario);
        Task<bool> AutenticarUtilizador(string codigo, string senha);

        //Métodos SubMontagens
        Task<List<Montagem>> GetMontagens();


        //Métodos SubMoveis
        Task<List<Movel>> GetMoveis();
        Task PutMovel(Movel movel);
        Task<List<Etapa>> GetEtapas();
        Task<Etapa> GetEtapa(int codEtapa);
        Task PutEtapa(Etapa etapa);


        //Métodos SubMateriais
        Task<List<Material>> GetMateriais();
        Task PutMaterial(Material material);


        //Métodos SubEncomendas
        Task<List<Encomenda>> GetEncomendas();
    }
}