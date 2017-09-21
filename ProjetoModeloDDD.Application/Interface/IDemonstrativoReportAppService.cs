using ProjetoModeloDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoModeloDDD.Application.Interface
{
    public interface  IDemonstrativoReportAppService

    {
        DemonstrativoReport CriarDemonstrativoReport(Producao producao, ITaxaDoacaoAppService taxaDoacao, ITaxaExtraProfissionalAppService taxaExtra, DateTime dataInicial, DateTime dataFinal);
        List<DemonstrativoReport> GerarLista(IEnumerable<Producao> producaoLista, ITaxaDoacaoAppService taxaDoacao, ITaxaExtraProfissionalAppService taxaExtra, DateTime dataInicial, DateTime dataFinal);



    }
}
