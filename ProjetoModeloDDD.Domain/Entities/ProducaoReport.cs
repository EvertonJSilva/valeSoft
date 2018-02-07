using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoModeloDDD.Domain.Entities
{
    public class ProducaoReport
    {
        //########### NOVO CAMPOS
        //Matricula = producao.Consulta.Profissional.Matricula,
        //        NomeProfissional = producao.Consulta.Profissional.NomeProfissional,
        //        DataIngresso = producao.Consulta.Profissional.DataIngresso,
        //        INSS = producao.Consulta.Profissional.INSS,
        //        CPF = producao.Consulta.Profissional.Cpf,
        //        ValorConsulta = producao.Consulta.ValorConsulta,
        //        ValorCopart = producao.Consulta.ValorCopart,
        //        ValorConvenio = producao.Consulta.ValorConvenio,
        //        TaxaBancaria = producao.Consulta.Profissional.TaxaBancaria,


        public string nomePaciente { get; set; }
        public string carteirinhaPaciente { get; set; }
        public string sessaoConsulta { get; set; }
        public decimal valorConsulta { get; set; }
        public DateTime dataInicial { get; set; }
        public DateTime dataFinal { get; set; }

        public ProducaoReport(DTOProducao producao)
        {
            this.nomePaciente = producao.nomePaciente;
            nomePaciente.ToUpper();
            this.carteirinhaPaciente = producao.carteirinhaPaciente;
            this.sessaoConsulta = producao.sessaoConsulta;
            this.valorConsulta = producao.valorConvenio;
            this.dataInicial = producao.dataInicial;
            this.dataFinal = producao.dataFinal;
        }

        static public List<ProducaoReport> GerarLista(IEnumerable<DTOProducao> producaoLista, DateTime dataInicial, DateTime dataFinal)
        {
            List<ProducaoReport> lista = new List<ProducaoReport>();
            
            foreach (DTOProducao producao in producaoLista)
            {
                producao.dataInicial = dataInicial;
                producao.dataFinal = dataFinal;

                if (producao.revisado == true) { 

                lista.Add(new ProducaoReport(producao));
                }
            }

            return lista;
        }


    }

}



