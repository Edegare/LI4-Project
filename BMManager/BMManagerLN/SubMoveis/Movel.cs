using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMManagerLN.SubMoveis
{
    public class Movel
    {
        [Key]
        public int Numero { get; set; }

        [Required(ErrorMessage = "Tem que selecionar um nome")]
        [StringLength(75, ErrorMessage = "Nome tem mais de 75 caracteres")]
        public String Nome { get; set; }

        public int Quantidade { get; set; } = 0;
<<<<<<< HEAD

        [Required(ErrorMessage = "Tem que selecionar uma imagem")]
        //falta ver se é uma imagem
        public String Imagem { get; set; }

        [NotMapped]
        public Dictionary<int, Etapa> Etapas_Montagem { get; set; } = new Dictionary<int, Etapa>(); //
=======
        public byte[] Imagem { get; set; }
        public Dictionary<int, int> Etapas_Montagem { get; set; } = new Dictionary<int, int>(); //
>>>>>>> 01d059c (Registar móvel)
    }
}
