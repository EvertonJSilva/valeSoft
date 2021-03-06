﻿using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoModeloDDD.Infra.Data.Repositories
{
    public class TaxaExtraProfissionalRepository : RepositoryBase<TaxaExtraProfissional>, ITaxaExtraProfissionalRepository
    {

        
       public IEnumerable<TaxaExtraProfissional> GetPorIdTaxaExtraProfissional(int id)
        {
            return Db.TaxasExtrasProfissionais.Where(t => t.ProfissionalId == id);
        }         
                
       

    }
}
