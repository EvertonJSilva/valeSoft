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
    public class ProfissionalAppService : AppServiceBase<Profissional>, IProfissionalAppService
    {

        private readonly IProfissionalService _profissionalService;

        public ProfissionalAppService(IProfissionalService profissionalService)
            : base(profissionalService)
        {
            _profissionalService = profissionalService;
        }
    }
}
