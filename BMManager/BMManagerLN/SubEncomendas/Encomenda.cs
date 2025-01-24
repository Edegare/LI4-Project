using BMManagerLN.SubMontagens;
using BMManagerLN.SubMoveis;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMManagerLN.SubEncomendas
{
    public class Encomenda
    {
        [Key]
        public int Numero { get; set; }
        [Required(ErrorMessage="O campo Cliente é obrigatório!")]
        public String Cliente { get; set; }
        [Required(ErrorMessage = "O campo Data é obrigatório!")]
        public DateOnly Data_Prevista { get; set; }
        public DateOnly? Data_Real { get; set; }
        public bool Concluida { get; set; } = false;
    }
}
