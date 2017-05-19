using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Services;

namespace ProjetoModeloDDD.Application.Interface
{
    public class PacienteAppService : AppServiceBase<Paciente>, IPacienteAppService
    {

        private readonly IPacienteService _pacienteService;

        public PacienteAppService(IPacienteService pacienteService)
            : base(pacienteService)
        {
            _pacienteService = pacienteService;
        }
    }
}
