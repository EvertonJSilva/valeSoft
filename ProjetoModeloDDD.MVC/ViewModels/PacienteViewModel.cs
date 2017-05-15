using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetoModeloDDD.MVC.ViewModels
{
    public class PacienteViewModel
    {

        public string palavra { get; set; }
        public int localizarPor { get; set; }

        [Key]
        public int PacienteId { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "Preencha o campo Nome do Paciente")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Minimo {0} caracteres")]
        public string NomePaciente { get; set; }


        [DisplayName("Telefone 1")]
        [Required(ErrorMessage = "Preencha o campo Telefone do Paciente")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Minimo {0} caracteres")]
        public string TelefonePaciente1 { get; set; }


        [DisplayName("Telefone 2")]
        public string TelefonePaciente2 { get; set; }


        [DisplayName("Nr da carteira")]
        [Required(ErrorMessage = "Preencha o campo Carteirinha do Paciente")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Minimo {0} caracteres")]
        public string CarteirinhaPaciente { get; set; }

        [DisplayName("Tem coparticipação?")]
        public bool CopartPaciente { get; set; }
    }
}