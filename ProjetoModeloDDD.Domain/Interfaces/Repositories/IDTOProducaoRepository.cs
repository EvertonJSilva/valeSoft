
using ProjetoModeloDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace ProjetoModeloDDD.Domain.Interfaces.Repositories
{
    public interface IDTOProducaoRepository : IRepositoryBase<DTOProducao>
    {
        DTOProducao GetPorConsultaID(int id);

        IEnumerable<DTOProducao> GetListaPorData(DateTime datainicial, DateTime dataFinal, int idProfissional, string nomePaciente);
    }
}


