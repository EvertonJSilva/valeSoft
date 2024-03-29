﻿
using System.Collections.Generic;
using ProjetoModeloDDD.Domain.Entities;

namespace ProjetoModeloDDD.Domain.Interfaces.Services
{
    public interface IConsultaService : IServiceBase<Consulta>
    {

        IEnumerable<Consulta> ObterConsultasPorPaciente(Paciente paciente);
        IEnumerable<Consulta> ObterConsultasPorProfissional(Profissional profissional);
        IEnumerable<Consulta> GetPorIdProfissional(int id, string nomePaciente, string numeroliberacao);


    }
}
