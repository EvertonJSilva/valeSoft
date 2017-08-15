using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoModeloDDD.Domain.Entities
{
    public class ProducaoReport
    {
        public string nomePaciente { get; set; }
        public string carteirinhaPaciente { get; set; }
        public string sessaoConsulta { get; set; }
        public decimal valorConsulta { get; set; }
        public DateTime dataInicial { get; set; }
        public DateTime dataFinal { get; set; }

        public ProducaoReport(Producao producao)
        {
            this.nomePaciente = producao.Consulta.Liberacao.Paciente.NomePaciente;
            nomePaciente.ToUpper();
            this.carteirinhaPaciente = producao.CarteirinhaPaciente;
            this.sessaoConsulta = producao.Consulta.TipoSessao;
            this.valorConsulta = producao.Consulta.ValorConvenio;
            this.dataInicial = producao.dataInicial;
            this.dataFinal = producao.dataFinal;
        }

        static public List<ProducaoReport> GerarLista(IEnumerable<Producao> producaoLista)
        {
            List<ProducaoReport> lista = new List<ProducaoReport>();
            
            foreach (Producao producao in producaoLista)
            {
                if (producao.revisado == true) { 
                lista.Add(new ProducaoReport(producao));
                }
            }

            return lista;
        }


    }

}



