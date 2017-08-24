using System;
using System.Collections.Generic;
using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Repositories;
using ProjetoModeloDDD.Domain.Interfaces.Services;

namespace ProjetoModeloDDD.Domain.Services
{
    public class ConsultaService : ServiceBase<Consulta>, IConsultaService
    {
        private readonly IConsultaRepository _consultaRepository;

        public ConsultaService(IConsultaRepository consultaRepository)
            : base(consultaRepository)
        {
            _consultaRepository = consultaRepository;
        }

        public IEnumerable<Consulta> GetPorIdProfissional(int id)
        {
           return _consultaRepository.GetPorIdProfissional(id);
        }

        public IEnumerable<Consulta> ObterConsultasPorPaciente(Paciente paciente)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Consulta> ObterConsultasPorProfissional(Profissional profissional)
        {
            throw new NotImplementedException();
        }
    }
}
