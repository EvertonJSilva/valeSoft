
using ProjetoModeloDDD.Domain.Entities;
using System.Collections.Generic;

namespace ProjetoModeloDDD.Domain.Interfaces.Services
{
    public interface ITaxaExtraProfissionalService : IServiceBase<TaxaExtraProfissional>
    {

        IEnumerable<TaxaExtraProfissional> GetPorIdTaxaExtraProfissional(int id);
    }
}
