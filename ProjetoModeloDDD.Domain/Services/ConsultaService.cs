using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Repositories;
using ProjetoModeloDDD.Domain.Interfaces.Services;

namespace ProjetoModeloDDD.Domain.Services
{
    public class ConsultaService : ServiceBase<Consulta>, IConsultaService
    {
        private readonly IConsultaRepository _consultaRepository;

        public ConsultaService(IConsultaRepository consultaRepository)
            : base(consultaRepository)
        {
            _consultaRepository = consultaRepository;
        }

        
    }
}
