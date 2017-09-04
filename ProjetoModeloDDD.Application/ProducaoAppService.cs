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
    public class ProducaoAppService : AppServiceBase<Producao>, IProducaoAppService
    {
        private readonly IProducaoService _producaoService;

        public ProducaoAppService(IProducaoService producaoService)
            : base(producaoService)
        {
            _producaoService = producaoService;
        }

        public Producao GetPorConsultaID(int id)
        {
            return _producaoService.GetPorConsultaID(id);
        }

        public IEnumerable<Producao> GetListaPorData(DateTime dataInicial, DateTime dataFinal,int idProfissional, string nomePaciente)
        {
            return _producaoService.GetListaPorData(dataInicial, dataFinal, idProfissional,nomePaciente);
        }

    }
}
