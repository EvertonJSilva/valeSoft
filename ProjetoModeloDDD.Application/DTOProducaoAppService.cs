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
    public class DTOProducaoAppService : AppServiceBase<DTOProducao>, IDTOProducaoAppService
    {
        private readonly IDTOProducaoService _producaoService;

        public DTOProducaoAppService(IDTOProducaoService producaoService)
            : base(producaoService)
        {
            _producaoService = producaoService;
        }

        public DTOProducao GetPorConsultaID(int id)
        {
            return _producaoService.GetPorConsultaID(id);
        }

        public IEnumerable<DTOProducao> GetListaPorData(DateTime dataInicial, DateTime dataFinal, int idProfissional, string nomePaciente)
        {
            return _producaoService.GetListaPorData(dataInicial, dataFinal, idProfissional, nomePaciente);
        }

    }
}
