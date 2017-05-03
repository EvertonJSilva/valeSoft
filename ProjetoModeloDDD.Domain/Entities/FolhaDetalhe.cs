using System;
using System.Collections.Generic;


namespace ProjetoModeloDDD.Domain.Entities
{
    public class FolhaDetalhe
    {
        public int FolhaDetalheId { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int FolhaId { get; set; }
        public virtual Folha Folha { get; set; }


    }
}
