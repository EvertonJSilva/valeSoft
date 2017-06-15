using ProjetoModeloDDD.Domain.Entities;
using System.Collections.Generic;

namespace ProjetoModeloDDD.Application.Interface
{
    public interface IValorConsultaAppService : IAppServiceBase<ValorConsulta>
    {
        IEnumerable<ValorConsulta> GetPorSigla(string sigla);
    }
}
