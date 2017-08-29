using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Repositories;
using ProjetoModeloDDD.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoModeloDDD.Domain.Services
{
    public class TaxaExtraProfissionalService : ServiceBase<TaxaExtraProfissional>, ITaxaExtraProfissionalService
    {
        private readonly ITaxaExtraProfissionalRepository _taxaRepository;

        public TaxaExtraProfissionalService(ITaxaExtraProfissionalRepository taxaRepository)
            : base(taxaRepository)
        {
            _taxaRepository = taxaRepository;
           
        }

        public TaxaExtraProfissional GetPorIdTaxaExtraProfissional(int id)
        {
            return _taxaRepository.GetPorIdTaxaExtraProfissional(id);
        }
   }
}
