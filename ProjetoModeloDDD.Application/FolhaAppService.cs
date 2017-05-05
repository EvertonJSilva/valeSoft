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
    public class FolhaAppService : AppServiceBase<Folha>, IFolhaAppService
    {
        private readonly IFolhaService _folhaService;

        public FolhaAppService(IFolhaService folhaService)
            : base(folhaService)
        {
            _folhaService = folhaService;
        }

    }
}
