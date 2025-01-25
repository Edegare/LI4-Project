using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMManagerLN.SubMoveis
{
    public class Etapa
    {
        [Key]
        public int Codigo_Etapa { get; set; }

        [Required(ErrorMessage = "Tem que selecionar uma imagem")]
        public byte[]? Imagem { get; set; }

        public int Numero { get; set; }

        public int? Proxima_Etapa { get; set; }

        public int Movel { get; set; }
    }
}
