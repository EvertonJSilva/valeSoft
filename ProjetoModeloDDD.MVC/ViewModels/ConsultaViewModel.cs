using ProjetoModeloDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetoModeloDDD.MVC.ViewModels
{
    public class ConsultaViewModel
    {
        [Key]
        public int ConsultaId { get; set; }
        [Required(ErrorMessage = "Preencha o campo Autorizacao")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Minimo {0} caracteres")]
        public string Autorizacao { get; set; }

        [Required(ErrorMessage = "Preencha o campo Forma de Autorização")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Minimo {0} caracteres")]
        public string FormaAutorizar { get; set; }

        
        public DateTime DataHoraConsulta { get; set; }

        
        public decimal ValorConsulta { get; set; }

        [Required(ErrorMessage = "Preencha o campo Convenio")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Minimo {0} caracteres")]
        public string Convenio { get; set; }

        public decimal ValorCopart { get; set; }
        //valor que o convenio pagará de volta a cooperativa

        public decimal ValorConvenio { get; set; }

        [Required(ErrorMessage = "Preencha o campo Tipo Sessão")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Minimo {0} caracteres")]
        public string TipoSessao { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }
        
        
        //referencia a liberacao
        //public int LiberacaoId { get; set; }
        //public int ProfissionalId { get; set; }
        //referencia ao login que lançou a consulta
        public string Login { get; set; }

        public virtual Liberacao Liberacao { get; set; }
        public virtual Profissional Profissional { get; set; }


    }
}