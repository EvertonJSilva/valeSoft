using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoModeloDDD.MVC.ViewModels
{
    public class LocalizarViewModel
    {
        public string palavra { get; set; }
       public localizar localizarPor { get; set; }

           public class localizar
            {
            public string text { get; set; }
            public string value { get; set; }
            }

    }
}

