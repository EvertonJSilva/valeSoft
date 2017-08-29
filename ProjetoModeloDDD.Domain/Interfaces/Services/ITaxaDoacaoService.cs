
using System.Collections.Generic;
using ProjetoModeloDDD.Domain.Entities;

namespace ProjetoModeloDDD.Domain.Interfaces.Services
{
    public interface ITaxaDoacaoService : IServiceBase<TaxaDoacao>
    {
        IEnumerable<TaxaDoacao> GetPorIdTaxaProfissional(int id);
    }
}
