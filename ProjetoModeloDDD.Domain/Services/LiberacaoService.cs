using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Repositories;
using ProjetoModeloDDD.Domain.Interfaces.Services;

namespace ProjetoModeloDDD.Domain.Services
{
    public class LiberacaoService : ServiceBase<Liberacao>, ILiberacaoService
    {

        private readonly ILiberacaoRepository _liberacaoRepository;

        public LiberacaoService(ILiberacaoRepository liberacaoRepository)
            : base(liberacaoRepository)
        {
            _liberacaoRepository = liberacaoRepository;
        }
    }
}
