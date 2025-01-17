using BMManager.BMManagerCD;
using BMManagerLN.SubEncomendas;
using BMManagerLN.SubFuncionarios;
using BMManagerLN.SubMateriais;
using BMManagerLN.SubMontagens;
using BMManagerLN.SubMoveis;

namespace BMManagerLN
{
    public class BMManagerLN : APIBMManagerLN
    {

        //Subsistemas
        private APICSubFuncionarios subFuncionarios;
        //        private APICSubMontagens subMontagens;
        //        private APICSubMoveis subMoveis;
        //        private APICSubMateriais subMateriais;
        //        private APICSubEncomendas subEncomendas;

        //Construtor
        public BMManagerLN(BMManagerContext db)
        {
            subFuncionarios = new CSubFuncionarios(db);
            //            subMontagens = new CSubMontagens(db);
            //            subMoveis = new CSubMoveis(db);
            //            subMateriais = new CSubMateriais(db);
            //            subEncomendas = new CSubEncomendas(db);
        }

        //Métodos da lógica de negócio

        //Métodos SubFuncionários
        public Task<List<Funcionario>> GetFuncionarios()
        {
            return subFuncionarios.GetFuncionarios();
        }

        public Task PutFuncionario(Funcionario funcionario)
        {
            return subFuncionarios.PutFuncionario(funcionario);
        }

        public Task<bool> AutenticarUtilizador(string codigo, string senha)
        {
            return subFuncionarios.AutenticarUtilizador(codigo, senha);
        }

        //Métodos SubMontagens
        public Task<List<Montagem>> GetMontagens()
        {
            return null;
        }

        //Métodos SubMoveis
        public Task<List<Movel>> GetMoveis()
        {
            return null;
        }
        public Task<List<Etapa>> GetEtapas()
        {
            return null;
        }
        public Task<List<Etapa>> GetEtapa(int codEtapa)
        {
            return null;
        }

        public Task PutEtapa(Etapa etapa)
        {
            return null;
        }

        //Métodos SubMateriais
        public Task<List<Material>> GetMateriais()
        {
            return null;
        }

        public Task PutMaterial(Material material)
        {
            return null;
        }

        //Métodos SubEncomendas
        public Task<List<Encomenda>> GetEncomendas()
        {
            return null;
        }
    }
}
