using ProjetoModeloDDD.Domain.Entities;
using System.Collections.Generic;

namespace ProjetoModeloDDD.Application.Interface
{
    public interface ITaxaExtraProfissionalAppService : IAppServiceBase<TaxaExtraProfissional>
    {
        IEnumerable <TaxaExtraProfissional> GetPorIdTaxaExtraProfissional(int id);
    }
}
