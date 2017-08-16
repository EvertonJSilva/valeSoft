using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoModeloDDD.Infra.Data.Repositories
{
    public class ProducaoRepository : RepositoryBase<Producao>, IProducaoRepository
    {
        public Producao GetPorConsultaID(int id)
        {
          return   Db.Producoes.Where(p => p.Consulta.ConsultaId == id).Single();     
        }

        public IEnumerable<Producao> GetListaPorData(DateTime datainicial, DateTime dataFinal)
        {
            
            

            return Db.Producoes.Where(p => p.Consulta.DataHoraConsulta >= datainicial
                                && p.Consulta.DataHoraConsulta <= dataFinal);

            
        }

    }
}
