using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Repositories;

namespace ProjetoModeloDDD.Domain.Services
{
    public class LiberacaoService : ServiceBase<Liberacao>, ILiberacaoRepository
    {

        private readonly ILiberacaoRepository _liberacaoRepository;

        public LiberacaoService(ILiberacaoRepository liberacaoRepository)
            : base(liberacaoRepository)
        {
            _liberacaoRepository = liberacaoRepository;
        }
    }
}
