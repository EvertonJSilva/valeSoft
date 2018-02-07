using ProjetoModeloDDD.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ProjetoModeloDDD.Application.Interface
{
    public interface IDTOProducaoAppService : IAppServiceBase<DTOProducao>
    {
        DTOProducao GetPorConsultaID(int id);

        IEnumerable<DTOProducao> GetListaPorData(DateTime dataInicial, DateTime dataFinal, int idProfissional, string nomePaciente);
    }
}
