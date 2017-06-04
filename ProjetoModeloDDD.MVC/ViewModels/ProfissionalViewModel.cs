using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetoModeloDDD.MVC.ViewModels
{
    public class ProfissionalViewModel
    {
        [Key]
        public int ProfissionalId { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "Preencha o campo Nome")]
        [MaxLength(250, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        public string NomeProfissional { get; set; }

        [DisplayName("Profissional")]
        public int TipoProfissionalId { get; set; }

        public virtual TipoProfissionalViewModel TipoProfissional { get; set; }

        [Required(ErrorMessage = "Preencha o campo Login")]
        [MaxLength(250, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Preencha o campo Senha")]
        [MaxLength(250, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        public string Senha { get; set; }
        
        [Required(ErrorMessage = "Preencha o campo CPF")]
        [MaxLength(250, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Preencha o campo INSS")]
        [MaxLength(250, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        public string INSS { get; set; }

        [DisplayName("Data de Ingresso")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage ="Preencha a data de ingresso")]
        public DateTime DataIngresso { get; set; }

        [DisplayName("Matricula")]
        [Required(ErrorMessage ="Preencha a Matricula")]
        public int Matricula { get; set; }



    }
}