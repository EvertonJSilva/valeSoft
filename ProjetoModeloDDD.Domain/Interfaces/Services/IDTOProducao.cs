using ProjetoModeloDDD.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ProjetoModeloDDD.Domain.Interfaces.Services
{
    public interface IDTOProducaoService : IServiceBase<DTOProducao>
    {
        DTOProducao GetPorConsultaID(int id);

        IEnumerable<DTOProducao> GetListaPorData(DateTime datainicial, DateTime dataFinal, int idProfissional, string nomePaciente);

    }
}
