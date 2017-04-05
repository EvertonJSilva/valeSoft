using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoModeloDDD.Domain.Entities
{
    public class Usuario
    {
 
        public Usuario(string usuario, string senha){
            this.nome = usuario;
            this.senha = senha;
        }
        private string nome { get; set; }
        private string senha { get; set; }
    }
}
