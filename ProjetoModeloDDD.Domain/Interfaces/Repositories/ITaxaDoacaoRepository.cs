using ProjetoModeloDDD.Domain.Entities;
using System.Collections.Generic;

namespace ProjetoModeloDDD.Domain.Interfaces.Repositories
{
    public interface ITaxaDoacaoRepository : IRepositoryBase<TaxaDoacao>
    {

        IEnumerable<TaxaDoacao> GetPorIdTaxaProfissional(int id);
    }
}
