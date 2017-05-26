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
        [Required(ErrorMessage = "Preencha o campo Profissional")]
        [MaxLength(150, ErrorMessage = "Máximo 150 caracteres")]
        [MinLength(2, ErrorMessage = "Minimo 2 caracteres")]   
        public string TipoProfissional { get; set; }

        [DisplayName("Valor")]
        [Required(ErrorMessage = "Preencha o campo Valor")]
        public decimal Valor { get; set; }
        
        [Key]
        public int TaxaDoacaoId { get; set; }


    }
}