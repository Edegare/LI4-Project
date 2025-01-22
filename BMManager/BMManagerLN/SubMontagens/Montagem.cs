using BMManagerLN.SubFuncionarios;
using BMManagerLN.SubMoveis;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMManagerLN.SubMontagens
{
    public enum Estado
    {
        Cancelada,
        [Description("Concluída")]
        Concluida,
        [Description("Em Progresso")]
        Em_Progresso,
        [Description("Em Pausa")]
        Em_Pausa
    }
    public class Montagem
    {
        [Key]
        public int Numero { get; set; }

        [Required]
        public DateTime Data_Inicial { get; set; }

        public DateTime? Data_Final { get; set; }

        public TimeSpan Duracao { get; set; } = TimeSpan.Zero;

        public Estado Estado { get; set; } = Estado.Em_Progresso;

        public bool Etapa_Concluida { get; set; } = false;

        [Required]
        public int Movel { get; set; }

        [Required]
        public int Etapa { get; set; }

        public int? Encomenda { get; set; }
    }
}
