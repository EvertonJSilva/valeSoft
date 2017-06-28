using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoModeloDDD.Domain.Entities
{
    public class Producao
    {

        public int ProducaoId { get; set; }
        //public string NomePaciente { get; set; }
        public string CarteirinhaPaciente { get; set; }
        //public string Sessao { get; set; }
        //public decimal ValorConvenio { get; set; }
        public bool revisado { get; set; }
        public bool Consolidado { get; set; }
        public DateTime dataInicial { get; set; }
        public DateTime dataFinal { get; set; }
        public int ConsultaId { get; set; }
        public virtual Consulta Consulta { get; set; }
        public virtual Profissional Profissional { get; set; }
         




    }
}
