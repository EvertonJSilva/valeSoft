using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Repositories;
using ProjetoModeloDDD.Domain.Interfaces.Services;

namespace ProjetoModeloDDD.Domain.Services
{
    public class ProfissionalService : ServiceBase<Profissional>, IProfissionalService
    {

        private readonly IProfissionalRepository _profissionalRepository;

        public ProfissionalService(IProfissionalRepository profissionalRepository)
            : base(profissionalRepository)
        {
            _profissionalRepository = profissionalRepository;
        }
    }
}
