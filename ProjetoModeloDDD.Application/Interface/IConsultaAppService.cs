using ProjetoModeloDDD.Domain.Entities;
using System.Collections.Generic;

namespace ProjetoModeloDDD.Application.Interface
{
    public interface IConsultaAppService : IAppServiceBase<Consulta>
    {
        IEnumerable<Consulta> GetPorIdProfissional(int id, string nomePaciente, string numeroliberacao);
    }
}
