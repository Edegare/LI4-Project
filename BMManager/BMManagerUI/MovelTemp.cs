using BMManagerLN.SubMoveis;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace BMManager.BMManagerUI
{
    public class MovelTemp
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório!")]
        [StringLength(75, ErrorMessage = "Nome não pode ter mais de 75 carateres!")]
        public string NovoMovelNome { get; set; }

        [Required(ErrorMessage = "O campo Imagem é obrigatório!")]
        public byte[] NovoMovelImagem { get; set; }
        public Dictionary<int, (byte[], int, List<(int,int)>)> NovoMovelEtapas { get; set; } = new Dictionary<int, (byte[], int, List<(int,int)>)>();
        public int EtapasRegistadas { get; set; } = 0;
        public void ResetMovelTemp()
        {
            NovoMovelNome = "";
            NovoMovelImagem = [];
            NovoMovelEtapas.Clear();
            EtapasRegistadas = 0;
        }

    }
}
