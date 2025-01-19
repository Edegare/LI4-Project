namespace BMManagerLN.SubMontagens
{
    public interface APICSubMontagens
    {
        Task<List<Montagem>> GetMontagens();
        Task PutMontagem(Montagem montagem);
    }
}
