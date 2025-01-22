using BMManagerLN.SubMateriais;

namespace BMManagerLN.SubMontagens
{
    public interface APICSubMontagens
    {
        int OrdenarEstado(Estado estado);
        Task<List<Montagem>> GetMontagens();
        Task<Montagem> GetMontagem(int codMontagem);
        Task PutMontagem(Montagem montagem);
        Task AssociarAEncomenda(int codMontagem, int codEncomenda);
        Task AtualizaMontagem(Montagem montagem);
    }
}
