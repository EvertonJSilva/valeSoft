using System;
using System.Collections.Generic;


namespace ProjetoModeloDDD.Domain.Entities
{
    public class TaxaDoacao
    {

        public virtual TipoProfissional TipoProfissional { get; set; }
        public int TipoProfissionalId { get; set; }
        public decimal Valor { get; set; }

        public int TaxaDoacaoId { get; set; }
        
    }
}
