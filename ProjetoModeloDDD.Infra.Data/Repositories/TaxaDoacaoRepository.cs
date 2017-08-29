using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoModeloDDD.Infra.Data.Repositories
{
    public class TaxaDoacaoRepository : RepositoryBase<TaxaDoacao>, ITaxaDoacaoRepository
    {

        public TaxaDoacao GetPorIdTaxaProfissional(int id)
        {
            return Db.TaxaDoacoes.Where(t => t.TipoProfissionalId == id).First();
        }

    }
}
