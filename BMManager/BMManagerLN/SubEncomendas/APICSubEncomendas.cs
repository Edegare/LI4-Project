
namespace BMManagerLN.SubEncomendas
{
    public interface APICSubEncomendas
    {
        Task<List<Encomenda>> GetEncomendas();
        Task<Encomenda> GetEncomenda(int codEncomenda);
        Task PutEncomenda(Encomenda encomenda);
        Task ConcluirEncomenda(int codEncomenda);
    }
}
