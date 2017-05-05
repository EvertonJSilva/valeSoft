using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Repositories;
using ProjetoModeloDDD.Domain.Interfaces.Services;

namespace ProjetoModeloDDD.Domain.Services
{
    public class ValorConsultaService : ServiceBase<ValorConsulta>, IValorConsultaService
    {
        private readonly IValorConsultaRepository _valorConsultaRepository;

        public ValorConsultaService(IValorConsultaRepository valorConsultaRepository)
            : base(valorConsultaRepository)
        {
            _valorConsultaRepository = valorConsultaRepository;
        }
    }
}
