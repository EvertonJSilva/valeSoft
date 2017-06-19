using ProjetoModeloDDD.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ProjetoModeloDDD.Domain.Interfaces.Services
{
    public interface IProducaoService : IServiceBase<Producao>
    {
        Producao GetPorConsultaID(int id);

        IEnumerable<Producao> GetListaPorData(DateTime datainicial, DateTime dataFinal);
        
   }
}
