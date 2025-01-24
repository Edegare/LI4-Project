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
        // Métodos SubFuncionários
        Task<List<Funcionario>> GetFuncionarios(); // Obtém a lista de todos os funcionários.
        Task<Funcionario> GetFuncionario(int codFuncionario); // Obtém os detalhes de um funcionário específico pelo código.
        Task PutFuncionario(Funcionario funcionario); // Atualiza os dados de um funcionário.
        Task ModificarFuncionario(int codFuncionario, string nome, string email, string senha, string telefone, Equipa? equipa, bool conta_ativa); // Modifica os detalhes de um funcionário específico.
        Task<FuncionarioDTO?> AutenticarUtilizador(string codigo, string senha); // Autentica um funcionário com código e senha.
        Task<List<Funcionario>> FuncionariosParticipamMontagem(int codMontagem); // Obtém os funcionários associados a uma montagem específica.
        Task<bool> AlterarSenha(int codigoFuncionario, string novaSenha); // Altera a senha de um funcionário.

        // Métodos SubMontagens
        int OrdenarEstado(Estado estado); // Retorna um número correspondente ao estado da montagem.
        Task<List<Montagem>> GetMontagens(); // Obtém a lista de todas as montagens.
        Task<Montagem> GetMontagem(int codMontagem); // Obtém os detalhes de uma montagem específica.
        Task<Montagem> NovaMontagem(int codMovel, int codFuncionario); // Cria uma nova montagem associada a um móvel e funcionário.
        Task<(int, int)> MateriaisSuficientesMontagem(int codMovel); // Verifica se há materiais suficientes para uma montagem.
        Task<Dictionary<Material, int>> MateriaisUtilizadosMontagem(int codMontagem); // Retorna os materiais utilizados na montagem.
        Task<Dictionary<Material, int>> MateriaisPorUtilizarMontagem(int codMontagem); // Retorna os materiais restantes para uma montagem.
        Task<string> AdicionarMontagensEncomenda(int codEncomenda, List<int> montagens); // Adiciona montagens a uma encomenda.
        Task<List<(int, string)>> GetMontagensEncomenda(int codEncomenda); // Obtém as montagens associadas a uma encomenda.
        Task TerminaEtapaMontagem(Montagem montagem); // Marca a etapa atual de uma montagem como concluída.
        Task SairEtapaMontagem(Montagem montagem); // Sai da etapa atual da montagem.
        Task<bool> ProximaEtapaMontagem(Montagem montagem); // Avança para a próxima etapa da montagem.
        Task ContinuaEtapaMontagem(Montagem montagem, int codFuncionario); // Continua uma etapa da montagem, associando um funcionário.
        Task<bool> MontagemTemMaisEtapas(Montagem montagem); // Verifica se a montagem possui mais etapas.
        Task CancelarMontagem(int codMontagem); // Cancela uma montagem.

        // Métodos SubMoveis
        Task<List<Movel>> GetMoveis(); // Obtém a lista de todos os móveis.
        Task<List<Movel>> GetMoveisSemImagens(); // Obtém a lista de móveis sem carregar imagens.
        Task<List<Encomenda_Precisa_Movel>> GetEncomendaPrecisaMovel(); // Obtém a lista de encomendas que precisam de móveis.
        Task<Movel> GetMovel(int codMovel); // Obtém os detalhes de um móvel específico.
        Task<Movel> GetMovelSemImagem(int codMovel); // Obtém os detalhes de um móvel sem carregar imagens.
        bool MovelExiste(int codMovel); // Verifica se um móvel existe.
        Task PutMovel(Movel movel); // Atualiza os dados de um móvel.
        Task<List<Etapa>> GetEtapas(); // Obtém a lista de etapas para montagem.
        Task<List<Etapa>> GetEtapasSemImagens(); // Obtém a lista de etapas sem carregar imagens.
        Task<Etapa> GetEtapa(int codEtapa); // Obtém os detalhes de uma etapa específica.
        Task<Dictionary<int, Etapa>> GetEtapasMovel(int codMovel); // Obtém as etapas de um móvel específico.
        Task<Dictionary<int, Etapa>> GetEtapasMovelSemImagens(int codMovel); // Obtém as etapas de um móvel sem carregar imagens.
        Task PutEtapa(Etapa etapa); // Atualiza os dados de uma etapa.
        Task<Dictionary<Movel, int>> GetMoveisEncomenda(int codEncomenda); // Obtém os móveis associados a uma encomenda.
        Task AdicionaMovelEncomenda(int codMovel, int quantidade, int codEncomenda); // Adiciona um móvel a uma encomenda.
        Task<Dictionary<Movel, int>> GetMoveisQueFaltamEncomenda(int codEncomenda); // Obtém os móveis que ainda faltam em uma encomenda.
        Task IncrementarQuantidadeMovel(int codMovel); // Incrementa a quantidade disponível de um móvel
      
        // Métodos SubMateriais
        Task<List<Material>> GetMateriais(); // Obtém a lista de todos os materiais.
        Task<List<Material>> GetMateriaisSemImagens(); // Obtém a lista de materiais sem carregar imagens.
        Task<Material> GetMaterial(int codMaterial); // Obtém os detalhes de um material específico.
        Task PutMaterial(Material material); // Atualiza os dados de um material.
        Task<Dictionary<Material, int>> GetMateriaisEtapa(int codEtapa); // Obtém os materiais necessários para uma etapa.
        Task AdicionaMaterialEtapa(int codMaterial, int quantidade, int codEtapa); // Adiciona material a uma etapa específica.
        Task AlterarQuantidadeMaterial(int codMaterial, int novaQuantidade); // Altera a quantidade de um material.
        Task<bool> MateriaisSuficientesEtapa(Montagem montagem); // Verifica se há materiais suficientes para uma etapa.

        // Métodos SubEncomendas
        Task<List<Encomenda>> GetEncomendas(); // Obtém a lista de todas as encomendas.
        Task<Encomenda> GetEncomenda(int codEncomenda); // Obtém os detalhes de uma encomenda específica.
        Task PutEncomenda(Encomenda encomenda); // Atualiza os dados de uma encomenda.
        Task<List<(int, string)>> GetMontagensNecessarias(int codEncomenda); // Obtém as montagens necessárias para uma encomenda.

        // Métodos Auxiliares
        string GetDescricao(Enum valor); // Obtém a descrição de um valor enumerado.
        public string ByteArrayParaImagem(byte[] bytes); // Converte um array de bytes em uma string de imagem.
    }
}