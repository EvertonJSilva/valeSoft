using ProjetoModeloDDD.Domain.Entities;
using System.Collections.Generic;

namespace ProjetoModeloDDD.Application.Interface
{
    public interface ITaxaDoacaoAppService : IAppServiceBase<TaxaDoacao>
    {
        TaxaDoacao GetPorIdTaxaProfissional(int id);
    }
}
