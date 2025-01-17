
namespace BMManagerLN.SubMoveis
{
    public class Movel
    {
        public int Numero { get; set; }
        public String Nome { get; set; }
        public int Quantidade { get; set; } = 0;
        public String Imagem { get; set; } //ver tipo
        public Dictionary<int, Etapa> Etapas_Montagem { get; set; } = new Dictionary<int, Etapa>(); //
    }
}
