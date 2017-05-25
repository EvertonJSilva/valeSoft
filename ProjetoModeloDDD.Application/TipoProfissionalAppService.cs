using System;
using System.Collections.Generic;
using ProjetoModeloDDD.Application.Interface;
using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Services;

namespace ProjetoModeloDDD.Application
{
    public class TipoProfissionalAppService : AppServiceBase<TipoProfissional>, ITipoProfissionalAppService
    {
        private readonly ITipoProfissionalService _TipoProfissionalService;

        public TipoProfissionalAppService(ITipoProfissionalService tipoProfissionalService)
            : base(tipoProfissionalService)
        {
            _TipoProfissionalService = tipoProfissionalService;
        }
    }
}
