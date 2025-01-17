
namespace BMManagerLN.SubMateriais
{
    public interface APICSubMateriais
    {
        Task<List<Material>> GetMateriais();
        Task PutMaterial(Material material);
    }
}
