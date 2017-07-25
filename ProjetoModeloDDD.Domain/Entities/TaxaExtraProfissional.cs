using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoModeloDDD.Domain.Entities
{
   public  class TaxaExtraProfissional
    {
        public int TaxaExtraProfissionalId { get; set; }
        public string tipo { get; set; }
        public string descricao { get; set; }
        public DateTime dataInsercao { get; set; }
        public DateTime dataCompensar { get; set; }
        public decimal valor { get; set; }

        public virtual Profissional Profissional { get; set; }
        public int ProfissionalId { get; set; }
    }
}
