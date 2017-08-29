using ProjetoModeloDDD.Domain.Entities;
using System.Collections.Generic;

namespace ProjetoModeloDDD.Domain.Interfaces.Repositories
{
    public interface ITaxaDoacaoRepository : IRepositoryBase<TaxaDoacao>
    {

        TaxaDoacao GetPorIdTaxaProfissional(int id);
    }
}
