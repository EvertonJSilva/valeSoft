using ProjetoModeloDDD.Domain.Entities;
using System.Collections.Generic;

namespace ProjetoModeloDDD.Domain.Interfaces.Repositories
{
    public interface ITaxaExtraProfissionalRepository : IRepositoryBase<TaxaExtraProfissional>
    {
        IEnumerable <TaxaExtraProfissional>  GetPorIdTaxaExtraProfissional(int id);
    }
}
