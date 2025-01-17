using BMManagerLN.SubMateriais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMManagerLN.SubMoveis
{
    public class Etapa
    {
        public int Codigo_Etapa { get; set; }
        public byte[] Imagem { get; set; }
        public int Numero { get; set; }
        public int Proxima_Etapa { get; set; } = -1;
        public int Movel { get; set; }
        public List<(int, int)> Materiais_Necessarios { get; set; } = new List<(int, int)>(); //
    }
}
