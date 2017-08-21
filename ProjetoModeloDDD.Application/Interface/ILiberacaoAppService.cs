﻿using ProjetoModeloDDD.Domain.Entities;

namespace ProjetoModeloDDD.Application.Interface
{
    public interface ILiberacaoAppService : IAppServiceBase<Liberacao>
    {

        void AtualizarConsultasRealizadas(Liberacao liberacao);
    }
}
