using ProjetoModeloDDD.Domain.Entities;
using System.Collections.Generic;

namespace ProjetoModeloDDD.Domain.Interfaces.Repositories
{
    public interface IValorConsultaRepository : IRepositoryBase<ValorConsulta>
    {
        IEnumerable<ValorConsulta> GetPorSigla(string sigla);
    }
}
