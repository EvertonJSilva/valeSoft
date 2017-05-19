using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Repositories;
using ProjetoModeloDDD.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoModeloDDD.Domain.Services
{
    public class ProducaoService : ServiceBase<Producao>, IProducaoService
    {
        private readonly IProducaoRepository _producaoRepository;

        public ProducaoService(IProducaoRepository producaoRepository)
            : base(producaoRepository)
        {
            _producaoRepository = producaoRepository;
        }



    }
}
