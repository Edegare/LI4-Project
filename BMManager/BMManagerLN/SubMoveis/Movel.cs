using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMManagerLN.SubMoveis
{
    public class Movel
    {
        [Key]
        public int Numero { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório!")]
        [StringLength(75, ErrorMessage = "Nome não pode ter mais de 75 carateres!")]
        public string Nome { get; set; }

        public int Quantidade { get; set; } = 0;

        [Required(ErrorMessage = "O campo Imagem é obrigatório!")]
        public byte[] Imagem { get; set; }
        [NotMapped]
        public Dictionary<int, int> Etapas_Montagem { get; set; } = new Dictionary<int, int>();
    }
}
