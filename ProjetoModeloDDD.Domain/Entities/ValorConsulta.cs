using System;
using System.Collections.Generic;


namespace ProjetoModeloDDD.Domain.Entities
{
    public class ValorConsulta
    {
        //sigla = 4 primeiros digitos da carteirinha
        public string Sigla { get; set; }
        public decimal Valor { get; set; }
        public string Classificacao { get; set; }
        public string Sessao { get; set; }
        public int ValorConsultaID { get; set; }
    }
}
