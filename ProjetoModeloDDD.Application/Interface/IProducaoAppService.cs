using ProjetoModeloDDD.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ProjetoModeloDDD.Application.Interface
{
    public interface IProducaoAppService : IAppServiceBase<Producao>
    {
        Producao GetPorConsultaID(int id);

        IEnumerable<Producao> GetListaPorData(DateTime dataInicial, DateTime dataFinal, int idProfissional, string nomePaciente);
    }
}
