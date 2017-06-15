using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoModeloDDD.Infra.Data.Repositories
{
    public class ValorConsultaRepository : RepositoryBase<ValorConsulta>, IValorConsultaRepository
    {
        public IEnumerable<ValorConsulta> GetPorSigla(string sigla)
        {
            return Db.ValorConsultas.Where(p => p.Sigla  == sigla);
        }

    }
}
