using ProjetoModeloDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace ProjetoModeloDDD.MVC.ViewModels
{
    public class TipoProfissionalViewModel
    {
        [Key]
        public int TipoProfissionalID { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "Preencha o campo descrição")]
        [MaxLength(150, ErrorMessage = "Máximo 150 caracteres")]
        [MinLength(2, ErrorMessage = "Minimo 2 caracteres")]
        public string Descricao { get; set; }


        [DisplayName("Nível de acesso")]
        [Required(ErrorMessage = "Preencha o campo nível de acesso")]
        public int nivelAcesso { get; set; }
    }
}