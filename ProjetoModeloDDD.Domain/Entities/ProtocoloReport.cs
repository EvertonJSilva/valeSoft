using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoModeloDDD.Domain.Entities
{
    public class ProtocoloReport
    {

        public decimal ValorConvenio { get; set; }

        public ProtocoloReport(Producao producao)
        {
            this.ValorConvenio = producao.Consulta.ValorConvenio;
        }

        static public List<ProtocoloReport> GerarLista(IEnumerable<Producao> producaoLista)
        {
            List<ProtocoloReport> lista = new List<ProtocoloReport>();

            foreach (Producao producao in producaoLista)
            {
                lista.Add(new ProtocoloReport(producao));

            }

            return lista;
        }


    }
}
