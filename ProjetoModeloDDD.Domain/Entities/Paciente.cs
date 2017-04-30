
namespace ProjetoModeloDDD.Domain.Entities
{
    public class Paciente
    {
        public int PacienteId { get; set; }
        public string NomePaciente { get; set; }
        public string TelefonePaciente1 { get; set; }
        public string TelefonePaciente2 { get; set; }
        public string CarteirinhaPaciente { get; set; }
        public bool CopartPaciente { get; set; }


    }
}
