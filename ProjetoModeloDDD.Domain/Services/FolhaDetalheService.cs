using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Repositories;
using ProjetoModeloDDD.Domain.Interfaces.Services;

namespace ProjetoModeloDDD.Domain.Services
{
    public class FolhaDetalheService : ServiceBase<FolhaDetalhe>, IFolhaDetalheService
    {

        private readonly IFolhaDetalheRepository _folhaDetalheRepository;

        public FolhaDetalheService(IFolhaDetalheRepository folhaDetalheRepository)
            : base(folhaDetalheRepository)
        {
            _folhaDetalheRepository = folhaDetalheRepository;
        }
    }
}
