using System.ComponentModel.DataAnnotations;

namespace BMManagerLN.SubMateriais
{
    public class Etapa_Precisa_Material
    {
        [Key]
        public int Etapa { get; set; }

        [Key]
        public int Material { get; set; }

        public int Quantidade { get; set; } = 0;
    }
}
