using System;
using System.Collections.Generic;
using System.Linq;

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
        public decimal ValorDoacao { get; set; }
        public decimal ValorOutrosDescontos { get; set; }
        public decimal ValorOutrosAcrecimos { get; set; }
        public decimal TaxaBancaria { get; set; }
        public DateTime dataInicial { get; set; }
        public DateTime dataFinal { get; set; }

        public int idProfissional { get; set; }


    }
}
