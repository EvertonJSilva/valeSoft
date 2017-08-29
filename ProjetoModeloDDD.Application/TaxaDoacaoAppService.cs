using ProjetoModeloDDD.Application.Interface;
using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoModeloDDD.Application
{
    public class TaxaDoacaoAppService : AppServiceBase<TaxaDoacao>, ITaxaDoacaoAppService
    {
        private readonly ITaxaDoacaoService _taxaDoacaoService;

        public TaxaDoacaoAppService(ITaxaDoacaoService taxaDoacaoService)
            : base(taxaDoacaoService)
        {
            _taxaDoacaoService = taxaDoacaoService;
        }

        public IEnumerable<TaxaDoacao> GetPorIdTaxaProfissional(int id)
        {
            return _taxaDoacaoService.GetPorIdTaxaProfissional(id);
        }

    }
}
