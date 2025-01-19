using BMManagerLN.SubMoveis;
using System.Runtime.CompilerServices;

namespace BMManager.BMManagerUI
{
    public class MovelTemp
    {
        public string NovoMovelNome { get; set; }
        public byte[] NovoMovelImagem { get; set; }
        public Dictionary<int, (byte[], int, List<(int,int)>)> NovoMovelEtapas { get; set; } = new Dictionary<int, (byte[], int, List<(int,int)>)>();
        public int EtapasRegistadas { get; set; } = 0;
    }
}
