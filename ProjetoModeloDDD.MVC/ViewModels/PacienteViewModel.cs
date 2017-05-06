using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetoModeloDDD.MVC.ViewModels
{
    public class PacienteViewModel
    {
        [Key]
        public int PacienteId { get; set; }

        [Required(ErrorMessage = "Preencha o campo Nome do Paciente")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Minimo {0} caracteres")]
        public string NomePaciente { get; set; }

        [Required(ErrorMessage = "Preencha o campo Telefone do Paciente")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Minimo {0} caracteres")]
        public string TelefonePaciente1 { get; set; }
        public string TelefonePaciente2 { get; set; }

        [Required(ErrorMessage = "Preencha o campo Carteirinha do Paciente")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Minimo {0} caracteres")]
        public string CarteirinhaPaciente { get; set; }
        public bool CopartPaciente { get; set; }


    }
}