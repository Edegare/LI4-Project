using BMManagerLN.SubEncomendas;
using BMManagerLN.SubFuncionarios;
using BMManagerLN.SubMoveis;

namespace BMManagerLN.SubMontagens
{
    public enum Estado
    {
        Cancelada,
        Concluida,
        Em_Progresso,
        Em_Pausa
    }
    public class Montagem
    {
        public int Numero { get; set; }
        public DateTime Data_Inicial { get; set; }
        public DateTime? Data_Final { get; set; }
        public TimeSpan Duracao { get; set; } = TimeSpan.Zero;
        public Estado Estado { get; set; }
        public bool Etapa_Concluida { get; set; } = false;
        public Movel Movel_Montado { get; set; } //
        public Etapa Etapa_Atual { get; set; } //
        public List<Funcionario> Funcionarios { get; set; } = new List<Funcionario>(); //
        public Encomenda? Encomenda { get; set; } //

    }
}
