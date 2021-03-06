﻿using ProjetoModeloDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetoModeloDDD.MVC.ViewModels
{
    public class ValorConsultaViewModel
    {
        //sigla = 4 primeiros digitos da carteirinha
        [DisplayName("Sigla da carteira")]
        [Required(ErrorMessage = "Preencha o campo Sigla")]
        [MaxLength(4, ErrorMessage = "Máximo 4 caracteres")]
        [MinLength(4, ErrorMessage = "Minimo 4 caracteres")]
        public string Sigla { get; set; }

        [DisplayName("Valor")]
        [Required(ErrorMessage = "Preencha o campo Valor")]
        public decimal Valor { get; set; }

        [DisplayName("Classificação")]
        [Required(ErrorMessage = "Preencha o campo Classificação")]
        [MaxLength(10, ErrorMessage = "Máximo 10 caracteres")]
        [MinLength(2, ErrorMessage = "Minimo 2 caracteres")]
        public string Classificacao { get; set; }

        [DisplayName("Sessão")]
        [Required(ErrorMessage = "Preencha o campo Sessão")]
        [MaxLength(8, ErrorMessage = "Máximo 8 caracteres")]
        [MinLength(8, ErrorMessage = "Minimo 8 caracteres")]
        public string Sessao { get; set; }


        [DisplayName("Solicta copart")]
        [Required(ErrorMessage = "Preencha o campo")]
        public bool temCopart { get; set; }

        [Key]
        public int ValorConsultaId { get; set; }

    }
}