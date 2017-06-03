using ProjetoModeloDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetoModeloDDD.MVC.ViewModels
{
    public class ConsultaViewModel
    {
        [Key]
        public int ConsultaId { get; set; }

        [DisplayName("Autorização")]
        [Required(ErrorMessage = "Preencha o campo Autorizacao")]
        [MaxLength(150, ErrorMessage = "Máximo 150 caracteres")]
        [MinLength(2, ErrorMessage = "Minimo 2 caracteres")]
        public string Autorizacao { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }

        [DisplayName("Forma autorizado")]
        [Required(ErrorMessage = "Preencha o campo Forma de Autorização")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Minimo {0} caracteres")]
        public string FormaAutorizar { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Data da consulta")]
        public DateTime DataHoraConsulta { get; set; }

        [DisplayName("Valor da consulta")]
        public decimal ValorConsulta { get; set; }

        [DisplayName("Convênio")]
        [Required(ErrorMessage = "Preencha o campo Convenio")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Minimo {0} caracteres")]
        public string Convenio { get; set; }

        [DisplayName("Valor co-participação")]
        public decimal ValorCopart { get; set; }

        [DisplayName("Valor do Convênio")]
        public decimal ValorConvenio { get; set; }

        [DisplayName("Tipo da sessão")]
        [Required(ErrorMessage = "Preencha o campo Tipo Sessão")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Minimo {0} caracteres")]
        public string TipoSessao { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }

        [DisplayName("Liberação")]
        //referencia a liberacao
        public int LiberacaoId { get; set; }

        [DisplayName("Profissional")]
        public int ProfissionalId { get; set; }

        //referencia ao login que lançou a consulta
        public string Login { get; set; }

    //    public virtual Liberacao Liberacao { get; set; }
     //   public virtual Profissional Profissional { get; set; }

        public virtual LiberacaoViewModel Liberacao { get; set; }

        public virtual ProfissionalViewModel Profissional { get; set; }

    }
}