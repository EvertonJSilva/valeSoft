using System;
using System.Collections.Generic;


namespace ProjetoModeloDDD.Domain.Entities
{
    public class Folha
    {
        public int FolhaId { get; set; }
        public DateTime Mes { get; set; }
        public DateTime Ano { get; set; }
        public Decimal ValorTotal { get; set; }
        public int ProfissionalId { get; set; }

        public virtual Profissional Profissional { get; set; }




    }
}
