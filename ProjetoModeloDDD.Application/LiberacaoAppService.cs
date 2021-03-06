﻿using ProjetoModeloDDD.Application.Interface;
using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoModeloDDD.Application
{
    public class LiberacaoAppService : AppServiceBase<Liberacao>, ILiberacaoAppService
    {
        private readonly ILiberacaoService _liberacaoService;

        public LiberacaoAppService(ILiberacaoService liberacaoService)
            : base(liberacaoService)
        {
            _liberacaoService = liberacaoService;
        }

        public void AtualizarConsultasRealizadas(Liberacao liberacao)
        {
            _liberacaoService.AtualizarConsultasRealizadas(liberacao);
        }

        public IEnumerable<Liberacao> GetPorIdProfissional(int id, string nomePaciente, string numeroliberacao)
        {
            return _liberacaoService.GetPorIdProfissional(id, nomePaciente, numeroliberacao);
        }
    }
}
