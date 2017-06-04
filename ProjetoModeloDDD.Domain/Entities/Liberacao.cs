

namespace ProjetoModeloDDD.Domain.Entities
{
    public class Liberacao
    {
        public int LiberacaoId { get; set; }
        public string NumeroLiberacao { get; set; }
        public int QuantidadeTotal { get; set; }
        public int QuantidadeRealizada { get; set; }
        public int PacienteId { get; set; }
        public string MedicoEncaminhante { get; set; }
        public string CRM { get; set; }
        public virtual Paciente Paciente { get; set; }
    }
}
