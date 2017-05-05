using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
