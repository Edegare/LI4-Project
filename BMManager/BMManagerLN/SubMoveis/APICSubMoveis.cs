
using BMManagerLN.SubMoveis;

namespace BMManagerLN.SubMoveis
{
    public interface APICSubMoveis
    {
        Task<List<Movel>> GetMoveis();
        Task<Movel> GetMovel(int codMovel);
        bool MovelExiste(int codMovel);
        Task PutMovel(Movel moveis);
        Task<List<Etapa>> GetEtapas();
        Task<Etapa> GetEtapa(int codEtapa);
        Task<Dictionary<int,Etapa>> GetEtapasMovel(int codMovel);
        Task<Dictionary<int, Etapa>> GetEtapasMovelCondicao(Func<Etapa,bool> condicao);
        Task PutEtapa(Etapa etapa);
    }
}
