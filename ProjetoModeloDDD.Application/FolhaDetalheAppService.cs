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
    public class FolhaDetalheAppService : AppServiceBase<FolhaDetalhe>, IFolhaDetalheAppService
    {
        private readonly IFolhaDetalheService _folhaDetalheService;

        public FolhaDetalheAppService(IFolhaDetalheService folhaDetalheService)
            : base(folhaDetalheService)
        {
            _folhaDetalheService = folhaDetalheService;
        }


    }
}
