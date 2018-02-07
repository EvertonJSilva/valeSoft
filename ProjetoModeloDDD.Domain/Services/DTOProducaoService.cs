﻿using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Repositories;
using ProjetoModeloDDD.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoModeloDDD.Domain.Services
{
    public class DTOProducaoService : ServiceBase<DTOProducao>, IDTOProducaoService
    {
        private readonly IDTOProducaoRepository _producaoRepository;

        public DTOProducaoService(IDTOProducaoRepository producaoRepository)
            : base(producaoRepository)
        {
            _producaoRepository = producaoRepository;
        }
        public IEnumerable<DTOProducao> GetListaPorData(DateTime dataInicial, DateTime dataFinal, int idProfissional, string nomePaciente)
        {
            return _producaoRepository.GetListaPorData(dataInicial, dataFinal, idProfissional, nomePaciente);
        }

        public DTOProducao GetPorConsultaID(int id)
        {
            return _producaoRepository.GetPorConsultaID(id);
        }
    }
}
