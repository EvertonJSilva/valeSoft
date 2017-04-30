using System;
using System.Collections.Generic;


namespace ProjetoModeloDDD.Domain.Entities
{
    public class Profissional
    {
        public int ProfissionalId { get; set; }
        public string NomeProfissional { get; set; }
        public string TipoProfissional { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Cpf { get; set; }
        public string INSS { get; set; }


    }
}
