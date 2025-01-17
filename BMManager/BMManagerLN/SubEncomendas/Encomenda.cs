using BMManagerLN.SubMontagens;
using BMManagerLN.SubMoveis;

namespace BMManagerLN.SubEncomendas
{
    public class Encomenda
    {
        public int Numero { get; set; }
        public String Cliente { get; set; }
        public DateOnly Data_Prevista { get; set; }
        public DateOnly? Data_Real { get; set; }
        public bool Concluida { get; set; } = false;
        public List<(Movel, int)> Moveis_Pedidos { get; set; } = new List<(Movel, int)>(); //
        public List<Montagem> Montagens_Associadas { get; set; } = new List<Montagem>(); //
    }
}
