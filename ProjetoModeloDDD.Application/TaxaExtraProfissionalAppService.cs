using ProjetoModeloDDD.Application.Interface;
using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Services;
using System.Collections.Generic;

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

        public IEnumerable <TaxaExtraProfissional> GetPorIdTaxaExtraProfissional(int id)
        {
            return _taxaService.GetPorIdTaxaExtraProfissional(id);
        }

    }
}
