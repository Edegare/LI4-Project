
namespace BMManagerLN.SubMoveis
{
    public interface APICSubMoveis
    {
        Task<List<Movel>> GetMoveis();
        Task<List<Movel>> GetMoveisSemImagens();
        Task<List<Encomenda_Precisa_Movel>> GetEncomendaPrecisaMovel();
        Task<Movel> GetMovel(int codMovel);
        Task<Movel> GetMovelSemImagem(int codMovel);
        bool MovelExiste(int codMovel);
        Task PutMovel(Movel moveis);
        Task<List<Etapa>> GetEtapas();
        Task<List<Etapa>> GetEtapasSemImagens();
        Task<Etapa> GetEtapa(int codEtapa);
        Task<Etapa> GetEtapaSemImagem(int codEtapa);
        Task<Dictionary<int,Etapa>> GetEtapasMovel(int codMovel);
        Task<Dictionary<int, Etapa>> GetEtapasMovelSemImagens(int codMovel);
        Task<Dictionary<int, Etapa>> GetEtapasMovelCondicao(Func<Etapa,bool> condicao);
        Task<Dictionary<int, Etapa>> GetEtapasMovelCondicaoSemImagem(Func<Etapa, bool> condicao);
        Task PutEtapa(Etapa etapa);
        Task<Dictionary<Movel, int>> GetMoveisEncomenda(int codEncomenda);
        Task AdicionaMovelEncomenda(int codMovel, int quantidade, int codEncomenda);
        Task IncrementarQuantidadeMovel(int codMovel);
    }
}
