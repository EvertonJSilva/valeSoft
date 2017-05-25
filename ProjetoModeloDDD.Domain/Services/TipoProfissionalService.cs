using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Repositories;
using ProjetoModeloDDD.Domain.Interfaces.Services;


namespace ProjetoModeloDDD.Domain.Services
{
    public class TipoProfissionalService : ServiceBase<TipoProfissional>, ITipoProfissionalService
    {
        private readonly ITipoProfissionalRepository _tipoProfissionalRepository;

        public TipoProfissionalService(ITipoProfissionalRepository tipoProfissionalRepository)
            : base(tipoProfissionalRepository)
        {
            _tipoProfissionalRepository = tipoProfissionalRepository;
        }
    }
}
