using ProjetoModeloDDD.Domain.Entities;
using System.Collections.Generic;

namespace ProjetoModeloDDD.Application.Interface
{
    public interface ITaxaDoacaoAppService : IAppServiceBase<TaxaDoacao>
    {
        IEnumerable<TaxaDoacao> GetPorIdTaxaProfissional(int id);
    }
}
