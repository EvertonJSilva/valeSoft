﻿
using System.Collections.Generic;
using ProjetoModeloDDD.Domain.Entities;

namespace ProjetoModeloDDD.Domain.Interfaces.Services
{
    public interface ILiberacaoService : IServiceBase<Liberacao>
    {
        void AtualizarConsultasRealizadas(Liberacao liberacao);
        IEnumerable<Liberacao> GetPorIdProfissional(int id, string nomePaciente, string numeroliberacao);
    }
}
