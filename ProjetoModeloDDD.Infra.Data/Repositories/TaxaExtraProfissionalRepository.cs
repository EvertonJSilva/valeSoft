using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Repositories;
using System.Linq;

namespace ProjetoModeloDDD.Infra.Data.Repositories
{
    public class TaxaExtraProfissionalRepository : RepositoryBase<TaxaExtraProfissional>, ITaxaExtraProfissionalRepository
    {

        
       public TaxaExtraProfissional GetPorIdTaxaExtraProfissional(int id)
        {
            return Db.TaxasExtrasProfissionais.Where(t => t.ProfissionalId == id).First();
        }         
                
       

    }
}
