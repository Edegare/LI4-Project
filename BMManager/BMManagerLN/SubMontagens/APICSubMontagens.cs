using BMManagerLN.SubMateriais;

namespace BMManagerLN.SubMontagens
{
    public interface APICSubMontagens
    {
        Task<List<Montagem>> GetMontagens();
        Task<Montagem> GetMontagem(int codMontagem);
        Task PutMontagem(Montagem montagem);
    }
}
