using ProjetoModeloDDD.Domain.Entities;
using System.Collections.Generic;

namespace ProjetoModeloDDD.Domain.Interfaces.Repositories
{
    public interface IConsultaRepository : IRepositoryBase<Consulta>
    {
        IEnumerable<Consulta> GetPorIdProfissional(int id,string nomePaciente, string numeroliberacao);
    }
}
