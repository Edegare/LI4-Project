
using BMManagerLN.SubMoveis;

namespace BMManagerLN.SubMoveis
{
    public interface APICSubMoveis
    {
        Task<List<Movel>> GetMoveis();
        Task<Movel> GetMovel(int codMovel);
        Task PutMovel(Movel moveis);
        Task<List<Etapa>> GetEtapas();
        Task<Etapa> GetEtapa(int codEtapa);
        Task PutEtapa(Etapa etapa);
    }
}
