using System;
using System.Collections.Generic;


namespace ProjetoModeloDDD.Domain.Entities
{
    public class TaxaDoacao
    {
        public string TipoProfissional { get; set; }
        public decimal Valor { get; set; }

        public int TaxaDoacaoID { get; set; }
        
    }
}
