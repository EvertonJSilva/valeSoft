using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoModeloDDD.Domain.Entities
{
    public class Usuario
    {
 
        public Usuario(string usuario, string senha, int NivelAcesso, int idProfissional){
            this.nome = usuario;
            this.senha = senha;
            this.nivelAcesso = nivelAcesso;
            this.idProfissional = idProfissional;

        }
        private string nome { get; set; }
        private string senha { get; set; }
        private int nivelAcesso { get; set; }
        private int idProfissional { get; set; }
    }
}
