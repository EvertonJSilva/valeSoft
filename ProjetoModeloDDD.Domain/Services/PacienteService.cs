using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Repositories;
using ProjetoModeloDDD.Domain.Interfaces.Services;

namespace ProjetoModeloDDD.Domain.Services
{
    public class PacienteService : ServiceBase<Paciente>, IPacienteService
    {

        private readonly IPacienteRepository _pacienteRepository;

        public PacienteService(IPacienteRepository pacienteRepository)
            : base(pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }
    }
}
