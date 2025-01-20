using System.ComponentModel.DataAnnotations;

namespace BMManagerLN.SubEncomendas
{
    public class Encomenda_Precisa_Movel
    {
        [Key]
        public int Encomenda { get; set; }

        [Key]
        public int Movel { get; set; }

        public int Quantidade { get; set; } = 0;
    }
}
