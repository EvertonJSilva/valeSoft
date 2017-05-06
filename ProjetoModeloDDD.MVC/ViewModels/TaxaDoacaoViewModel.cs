using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoModeloDDD.MVC.ViewModels
{
    public class TaxaDoacaoViewModel
    {
        public string TipoProfissional { get; set; }
        public decimal Valor { get; set; }

        public int TaxaDoacaoId { get; set; }


    }
}