using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoModeloDDD.Domain.Entities
{
    public class TipoProfissional
    {
        public int TipoProfissionalId { get; set; }
        public string Descricao { get; set; }
        public int nivelAcesso {get; set;}

}
}
