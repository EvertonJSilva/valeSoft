using ProjetoModeloDDD.Domain.Entities;
using System.Collections.Generic;

namespace ProjetoModeloDDD.Domain.Interfaces.Repositories
{
    public interface ILiberacaoRepository : IRepositoryBase<Liberacao>
    {
        IEnumerable<Liberacao> GetPorIdProfissional(int id);
    }
}
