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
        [Required]
        public String Cliente { get; set; }
        [Required]
        public DateOnly Data_Prevista { get; set; }
        public DateOnly? Data_Real { get; set; }
        public bool Concluida { get; set; } = false;
        [NotMapped]
        public List<(int, int)> Moveis_Pedidos { get; set; } = new List<(int, int)>(); //
        [NotMapped]
        public List<Montagem> Montagens_Associadas { get; set; } = new List<Montagem>(); //
    }
}
