using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BMManagerLN.SubFuncionarios
{
    public enum Equipa
    {
        Encomendas,
        [Description("Modelação")]
        Modelacao,
        Montagem,
        [Description("Armazém")]
        Armazem,
        [Description("Recursos Humanos")]
        Recursos_Humanos,
        [Description("Administração")]
        Administracao
    }
    public class Funcionario
    {
        [Key]
        public int Codigo_Utilizador { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório!")]
        [StringLength(75, ErrorMessage = "Nome tem mais de 75 caracteres!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório")]
        [StringLength(75, ErrorMessage = "Email tem mais de 30 caracteres!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Telefone é obrigatório!")]
        [RegularExpression(@"^[0-9]{9}$", ErrorMessage = "Telefone tem que conter 9 digitos!")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório!")]
        [StringLength(75, ErrorMessage = "Senha tem mais de 75 caracteres!")]
        public string Senha { get; set; } //alterar

        [Required(ErrorMessage = "O campo Equipa é obrigatório!")]
        [EnumDataType(typeof(Equipa), ErrorMessage = "Tem que selecionar uma equipa!")]
        public Equipa? Equipa { get; set; }

        public bool Conta_Ativa { get; set; } = true;
    }
}
