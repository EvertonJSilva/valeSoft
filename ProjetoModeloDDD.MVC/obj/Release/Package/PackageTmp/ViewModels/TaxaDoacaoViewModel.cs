using ProjetoModeloDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetoModeloDDD.MVC.ViewModels
{
    public class TaxaDoacaoViewModel
    {


        [DisplayName("Profissional")]
        public int TipoProfissionalId { get; set; }

        public virtual TipoProfissionalViewModel TipoProfissional { get; set; }

        [DisplayName("Valor")]
        [Required(ErrorMessage = "Preencha o campo Valor")]
        public decimal Valor { get; set; }
        
        [Key]
        public int TaxaDoacaoId { get; set; }


    }
}