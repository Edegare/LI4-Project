using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMManagerLN.SubMoveis
{
    public class Etapa
    {
        [Key]
        public int Codigo_Etapa { get; set; }

        [Required(ErrorMessage = "Tem que selecionar uma imagem")]
        //falta ver se é uma imagem
        public byte[]? Imagem { get; set; } //tirar nulo

//        [Required(ErrorMessage = "Tem que selecionar o numero da etapa")]
        public int Numero { get; set; }

        //        [Required(ErrorMessage = "Tem que selecionar uma próxima etapa")]
        public int? Proxima_Etapa { get; set; }

//        [Required(ErrorMessage = "Tem que selecionar um móvel")]
        public int Movel { get; set; }
    }
}
