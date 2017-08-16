using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ProjetoModeloDDD.MVC.ViewModels
{
    public class TaxaExtraProfissionalViewModel
    {
        
        [Key]
        public int TaxaExtraProfissionalId { get; set; }
        public string tipo { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "Preencha o campo a descrição")]
        [MaxLength(150, ErrorMessage = "Máximo 150 caracteres")]
        [MinLength(2, ErrorMessage = "Minimo 2 caracteres")]
        public string descricao { get; set; }

        public DateTime dataInsercao { get; set; }

        [Required(ErrorMessage = "Informe a data de vencimento")]
        public DateTime dataCompensar { get; set; }

        [Required(ErrorMessage = "Informe o valor")]
        public decimal valor { get; set; }
        
        public virtual TipoProfissionalViewModel Profissional { get; set; }
        
        public int ProfissionalId { get; set; }

    }
}