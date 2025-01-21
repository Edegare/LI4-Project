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
        Task ModificarFuncionario (int codFuncionario, string nome, string email, string senha, string telefone, Equipa? equipa, bool conta_ativa);
        Task<FuncionarioDTO?> AutenticarUtilizador(string codigo, string senha);
        Task<List<Funcionario>> FuncionariosParticipamMontagem(int codMontagem);

        //Métodos SubMontagens
        Task<List<Montagem>> GetMontagens();
        Task<Montagem> GetMontagem(int codMontagem);
        Task<Montagem> NovaMontagem(int codMovel, int codFuncionario);
        Task<(int, int)> MateriaisSuficientesMontagem(int codMovel);
        Task<Dictionary<Material, int>> MateriaisUtilizadosMontagem(int codMontagem);
        Task<Dictionary<Material, int>> MateriaisPorUtilizarMontagem(int codMontagem);
        Task<string> AdicionarMontagensEncomenda(int codEncomenda, List<int> montagens);
        Task<List<(int, string)>> GetMontagensEncomenda(int codEncomenda);


        //Métodos SubMoveis
        Task<List<Movel>> GetMoveis();
        Task<List<Encomenda_Precisa_Movel>> GetEncomendaPrecisaMovel();
        Task<Movel> GetMovel(int codMovel);
        bool MovelExiste(int codMovel);
        Task PutMovel(Movel movel);
        Task<List<Etapa>> GetEtapas();
        Task<Etapa> GetEtapa(int codEtapa);
        Task<Dictionary<int,Etapa>> GetEtapasMovel(int codMovel);
        Task PutEtapa(Etapa etapa);
        Task<Dictionary<Movel,int>> GetMoveisEncomenda(int codEncomenda);
        Task AdicionaMovelEncomenda(int codMovel, int quantidade, int codEncomenda);
        Task<Dictionary<Movel, int>> GetMoveisQueFaltamEncomenda(int codEncomenda);


        //Métodos SubMateriais
        Task<List<Material>> GetMateriais();
        Task<Material> GetMaterial(int codMaterial);
        Task PutMaterial(Material material);
        Task<Dictionary<Material, int>> GetMateriaisEtapa(int codEtapa);
        Task AdicionaMaterialEtapa(int codMaterial, int quantidade, int codEtapa);
        Task AlterarQuantidadeMaterial(int codMaterial, int novaQuantidade);


        //Métodos SubEncomendas
        Task<List<Encomenda>> GetEncomendas();
        Task<Encomenda> GetEncomenda(int codEncomenda);
        Task PutEncomenda(Encomenda encomenda);
        Task<List<(int, string)>> GetMontagensNecessarias(int codEncomenda);

        //Métodos Auxiliares
        string GetDescricao(Enum valor);

        public string ByteArrayParaImagem(byte[] bytes);
    }
}