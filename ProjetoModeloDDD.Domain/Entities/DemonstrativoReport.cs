using System;
using System.Collections.Generic;

namespace ProjetoModeloDDD.Domain.Entities
{
    public class DemonstrativoReport
    {

        public int Matricula { get; set; }
        public string NomeProfissional { get; set; }
        public DateTime DataIngresso { get; set; }
        public string INSS { get; set; }
        public string CPF { get; set; }
        public decimal ValorConsulta { get; set; }
        public decimal ValorCopart { get; set; }
        public decimal ValorConvenio { get; set; }

        public DemonstrativoReport(Producao producao)
        {
            this.Matricula = producao.Consulta.Profissional.Matricula;
            this.NomeProfissional = producao.Consulta.Profissional.NomeProfissional;
            this.DataIngresso = producao.Consulta.Profissional.DataIngresso;
            this.INSS = producao.Consulta.Profissional.INSS;
            this.CPF = producao.Consulta.Profissional.Cpf;
            this.ValorConsulta = producao.Consulta.ValorConsulta;
            this.ValorCopart = producao.Consulta.ValorCopart;
            this.ValorConvenio = producao.Consulta.ValorConvenio;

        }

        static public List<DemonstrativoReport> GerarLista(IEnumerable<Producao> producaoLista)
        {
            List<DemonstrativoReport> lista = new List<DemonstrativoReport>();

            foreach (Producao producao in producaoLista)
            {
                lista.Add(new DemonstrativoReport(producao));
            }

            return lista;
        }


    }
}
