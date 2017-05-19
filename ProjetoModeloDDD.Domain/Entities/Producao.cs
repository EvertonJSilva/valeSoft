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
        public string NomePaciente { get; set; }
        public string CarteirinhaPaciente { get; set; }
        public string Sessao { get; set; }
        public decimal ValorConvenio { get; set; }



    }
}
