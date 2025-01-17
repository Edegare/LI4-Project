using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMManagerLN.SubMateriais
{
    public class Material
    {
        public int Numero { get; set; }
        public String Nome { get; set; }
        public int Quantidade { get; set; } = 0;
        public byte[] Imagem { get; set; } = new byte[8294400]; //ver tipo
    }
}
