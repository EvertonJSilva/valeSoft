using ProjetoModeloDDD.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjetoModeloDDD.MVC.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Preencha o campo usuário")]
        public string usuario { get; set; }
        [Required(ErrorMessage = "Preencha o campo senha")]
        public string senha {
            get;
            set;
        }

        public string nome { get; set; }
        public int nivelAcesso { get; set; }
    }
}
