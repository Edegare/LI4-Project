using BMManager;
using BMManager.BMManagerCD;
using BMManager.BMManagerUI.Funcionarios;
using BMManagerLN.SubEncomendas;
using BMManagerLN.SubFuncionarios;
using BMManagerLN.SubMateriais;
using BMManagerLN.SubMontagens;
using BMManagerLN.SubMoveis;
using BMManagerLN.Exceptions;

namespace BMManagerLN
{
    public class BMManagerLN : APIBMManagerLN
    {

        //Subsistemas
        private APICSubFuncionarios subFuncionarios;
        private APICSubMontagens subMontagens;
        private APICSubMoveis subMoveis;
        private APICSubMateriais subMateriais;
        private APICSubEncomendas subEncomendas;

        //Construtor
        public BMManagerLN(BMManagerContext db)
        {
            subFuncionarios = new CSubFuncionarios(db);
            subMontagens = new CSubMontagens(db);
            subMoveis = new CSubMoveis(db);
            subMateriais = new CSubMateriais(db);
            subEncomendas = new CSubEncomendas(db);
        }

        //Métodos da lógica de negócio

        //Métodos SubFuncionários
        public Task<List<Funcionario>> GetFuncionarios()
        {
            return subFuncionarios.GetFuncionarios();
        }

        public Task<Funcionario> GetFuncionario(int codFuncionario)
        {
            return subFuncionarios.GetFuncionario(codFuncionario);
        }

        public Task PutFuncionario(Funcionario funcionario)
        {
            return subFuncionarios.PutFuncionario(funcionario);
        }

        public Task<FuncionarioDTO?> AutenticarUtilizador(string codigo, string senha)
        {
            return subFuncionarios.AutenticarUtilizador(codigo, senha);
        }

        //Métodos SubMontagens
        public Task<List<Montagem>> GetMontagens()
        {
            return subMontagens.GetMontagens();
        }

        public async Task NovaMontagem(int codMovel, int codFuncionario)
        {
            Movel movel = await subMoveis.GetMovel(codMovel);

            bool materiaisSuficientes = true; //alterar para método que verifica
            //verificar se existem materiais suficientes para primeira etapa ou todas
            if (!materiaisSuficientes)
            {
                throw new MateriaisInsuficientesException("MateriaisInsoficientes");
            }

            Montagem montagem = new Montagem();
            montagem.Movel = codMovel;
            montagem.Etapa = movel.Etapas_Montagem[1];
            montagem.Data_Inicial = DateTime.Now;

            await subMontagens.PutMontagem(montagem);
        }

        //Métodos SubMoveis
        public Task<List<Movel>> GetMoveis()
        {
            return subMoveis.GetMoveis();
        }

        public Task PutMovel(Movel movel)
        {
            return subMoveis.PutMovel(movel);
        }

        public Task<List<Etapa>> GetEtapas()
        {
            return subMoveis.GetEtapas();
        }
        public Task<Etapa> GetEtapa(int codEtapa)
        {
            return subMoveis.GetEtapa(codEtapa);
        }

        public Task PutEtapa(Etapa etapa)
        {
            return subMoveis.PutEtapa(etapa);
        }

        //Métodos SubMateriais
        public Task<List<Material>> GetMateriais()
        {
            return subMateriais.GetMateriais();
        }

        public Task PutMaterial(Material material)
        {
            return subMateriais.PutMaterial(material);
        }

        //Métodos SubEncomendas
        public Task<List<Encomenda>> GetEncomendas()
        {
            return subEncomendas.GetEncomendas();
        }
        public Task<Encomenda> GetEncomenda(int codEncomenda)
        {
            return subEncomendas.GetEncomenda(codEncomenda);
        }
        public Task PutEncomenda(Encomenda encomenda)
        {
            return subEncomendas.PutEncomenda(encomenda);
        }
    }
}
