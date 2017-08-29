
using ProjetoModeloDDD.Domain.Entities;

namespace ProjetoModeloDDD.Domain.Interfaces.Services
{
    public interface ITaxaExtraProfissionalService : IServiceBase<TaxaExtraProfissional>
    {

        TaxaExtraProfissional GetPorIdTaxaExtraProfissional(int id);
    }
}
