using ProjetoModeloDDD.Application.Interface;
using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoModeloDDD.Application
{
    public class ValorConsultaAppService : AppServiceBase<ValorConsulta>, IValorConsultaAppService
    {
        private readonly IValorConsultaService _valorConsultaService;

        public ValorConsultaAppService(IValorConsultaService valorConsultaService)
            : base(valorConsultaService)
        {
            _valorConsultaService = valorConsultaService;
        }


    }
}
