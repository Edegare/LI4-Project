using System.ComponentModel.DataAnnotations;

namespace BMManagerLN.SubFuncionarios
{
    public class Funcionario_Participa_Montagem
    {
        [Key]
        public int Montagem { get; set; }

        [Key]
        public int Funcionario { get; set; }
    }
}
