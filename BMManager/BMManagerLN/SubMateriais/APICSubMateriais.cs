
namespace BMManagerLN.SubMateriais
{
    public interface APICSubMateriais
    {
        Task<List<Material>> GetMateriais();
        Task<List<Material>> GetMateriaisSemImagens();
        Task<Material> GetMaterial(int codMaterial);
        Task<Material> GetMaterialSemImagem(int codMaterial);
        Task PutMaterial(Material material);
        Task<Dictionary<Material, int>> GetMateriaisEtapa(int codEtapa);
        Task<Dictionary<Material, int>> GetMateriaisEtapas(int[] codEtapas);
        Task AdicionaMaterialEtapa(int codMaterial, int quantidade, int codEtapa);
        Task AlterarQuantidadeMaterial(int codMaterial, int novaQuantidade);
        Task AtualizaStockMateriaisEtapa(int codEtapa);
    }
}
