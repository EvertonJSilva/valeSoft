using ProjetoModeloDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetoModeloDDD.MVC.ViewModels
{
    public class LiberacaoViewModel
    {
        [Key]
        public int LiberacaoId { get; set; }

        [DisplayName("Número")]
        [Required(ErrorMessage = "Preencha o campo Numero de Liberação")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Minimo {0} caracteres")]
        public string NumeroLiberacao { get; set; }


        [DisplayName("Quantidade Solicitada")]
        [Required(ErrorMessage = "Preencha o campo Quantidade Solicitada de Consultas")]
        public int QuantidadeTotal { get; set; }


        [DisplayName("Quantidade já realizado")]
        //essa deve vir padrão com o numero 0
        public int QuantidadeRealizada { get; set; }

        [DisplayName("Medico Encaminhante")]
        public string MedicoEncaminhante { get; set; }
        [DisplayName("CRM Medico Encaminhante")]
        public string CRM { get; set; }

        public int PacienteId { get; set; }
        public virtual PacienteViewModel Paciente { get; set; }

    }
}