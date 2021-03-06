﻿
using System.Collections.Generic;
using ProjetoModeloDDD.Domain.Entities;

namespace ProjetoModeloDDD.Domain.Interfaces.Services
{
    public interface IValorConsultaService : IServiceBase<ValorConsulta>
    {
        IEnumerable<ValorConsulta> GetPorSigla(string sigla);
    }
}
