﻿using ProjetoModeloDDD.Application.Interface;
using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Services;

namespace ProjetoModeloDDD.Application
{
    public class ConsultaAppService : AppServiceBase<Consulta>, IConsultaAppService
    {
        private readonly IConsultaService _consultaService;

        public ConsultaAppService(IConsultaService consultaService)
            : base(consultaService)
        {
            _consultaService = consultaService;
        }
    }
}
