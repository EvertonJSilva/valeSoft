using ProjetoModeloDDD.Application.Interface;
using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Services;


namespace ProjetoModeloDDD.Application
{
    public class TaxaExtraProfissionalAppService : AppServiceBase<TaxaExtraProfissional>, ITaxaExtraProfissionalAppService
    {
        private readonly ITaxaExtraProfissionalService _taxaService;

    public TaxaExtraProfissionalAppService(ITaxaExtraProfissionalService taxaService)
            : base(taxaService)
        {
            _taxaService = taxaService;
        }
    
    }
}
