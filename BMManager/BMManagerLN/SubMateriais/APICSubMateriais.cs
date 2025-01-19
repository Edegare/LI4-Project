
namespace BMManagerLN.SubMateriais
{
    public interface APICSubMateriais
    {
        Task<List<Material>> GetMateriais();
        Task<Material> GetMaterial(int codMaterial);
        Task PutMaterial(Material material);
    }
}
