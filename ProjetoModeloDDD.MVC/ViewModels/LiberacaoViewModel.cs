using ProjetoModeloDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetoModeloDDD.MVC.ViewModels
{
    public class LiberacaoViewModel
    {
        [Key]
        public int LiberacaoId { get; set; }

        [Required(ErrorMessage = "Preencha o campo Numero de Liberação")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Minimo {0} caracteres")]
        public string NumeroLiberacao { get; set; }

        [Required(ErrorMessage = "Preencha o campo Quantidade Total de Consultas")]
        public int QuantidadeTotal { get; set; }

        //essa deve vir padrão com o numero 0
        public int QuantidadeRealizada { get; set; }
        //public int PacienteId { get; set; }
        public virtual Paciente Paciente { get; set; }

    }
}