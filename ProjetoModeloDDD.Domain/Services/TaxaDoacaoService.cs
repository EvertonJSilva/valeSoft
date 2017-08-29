using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Repositories;
using ProjetoModeloDDD.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace ProjetoModeloDDD.Domain.Services
{
    public class TaxaDoacaoService : ServiceBase<TaxaDoacao>, ITaxaDoacaoService
    {
        private readonly ITaxaDoacaoRepository _taxaDoacaoRepository;

        public TaxaDoacaoService(ITaxaDoacaoRepository taxaDoacaoRepository)
            : base(taxaDoacaoRepository)
        {
            _taxaDoacaoRepository = taxaDoacaoRepository;
        }

        public TaxaDoacao GetPorIdTaxaProfissional(int id)
        {
            return _taxaDoacaoRepository.GetPorIdTaxaProfissional(id);
        }
    }
}
