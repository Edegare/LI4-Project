using System.ComponentModel.DataAnnotations;

namespace BMManagerLN.SubMateriais
{
    public class Material
    {
        [Key]
        public int Numero { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório!")]
        [StringLength(75, ErrorMessage = "Nome tem mais de 75 caracteres")]
        public string? Nome { get; set; }

        public int Quantidade { get; set; } = 0;

        [Required(ErrorMessage = "O campo Imagem é obrigatório!")]
        public byte[]? Imagem { get; set; } = null;
    }
}
