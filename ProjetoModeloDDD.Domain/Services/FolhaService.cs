using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Repositories;

namespace ProjetoModeloDDD.Domain.Services
{
    public class FolhaService : ServiceBase<Folha>, IFolhaRepository
    {

        private readonly IFolhaRepository _folhaRepository;

        public FolhaService(IFolhaRepository folhaRepository)
            : base(folhaRepository)
        {
            _folhaRepository = folhaRepository;
        }
    }
}
