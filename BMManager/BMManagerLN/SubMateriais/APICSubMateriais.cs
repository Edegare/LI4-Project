
namespace BMManagerLN.SubMateriais
{
    public interface APICSubMateriais
    {
        Task<List<Material>> GetMateriais();
        Task<Material> GetMaterial(int codMaterial);
        Task PutMaterial(Material material);
        Task<Dictionary<Material, int>> GetMateriaisEtapa(int codEtapa);
        Task<Dictionary<Material, int>> GetMateriaisEtapas(int[] codEtapas);
        Task AlterarQuantidadeMaterial(int codMaterial, int novaQuantidade);
    }
}
