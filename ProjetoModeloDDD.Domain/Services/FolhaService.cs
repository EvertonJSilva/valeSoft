using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Repositories;
using ProjetoModeloDDD.Domain.Interfaces.Services;

namespace ProjetoModeloDDD.Domain.Services
{
    public class FolhaService : ServiceBase<Folha>, IFolhaService
    {

        private readonly IFolhaRepository _folhaRepository;

        public FolhaService(IFolhaRepository folhaRepository)
            : base(folhaRepository)
        {
            _folhaRepository = folhaRepository;
        }
    }
}
