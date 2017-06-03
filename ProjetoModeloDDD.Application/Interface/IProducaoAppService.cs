﻿using ProjetoModeloDDD.Domain.Entities;

namespace ProjetoModeloDDD.Application.Interface
{
    public interface IProducaoAppService : IAppServiceBase<Producao>
    {
        Producao GetPorConsultaID(int id);
    }
}
