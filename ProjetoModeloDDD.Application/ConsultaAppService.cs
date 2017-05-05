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
