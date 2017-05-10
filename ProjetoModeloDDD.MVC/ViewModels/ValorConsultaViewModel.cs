﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoModeloDDD.MVC.ViewModels
{
    public class ValorConsultaViewModel
    {
        //sigla = 4 primeiros digitos da carteirinha
        public string Sigla { get; set; }
        public decimal Valor { get; set; }
        public string Classificacao { get; set; }
        public string Sessao { get; set; }
        public int ValorConsultaId { get; set; }

    }
}