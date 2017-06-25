using System;
using System.Collections.Generic;


namespace ProjetoModeloDDD.Domain.Entities
{
    public class Profissional
    {
        public int ProfissionalId { get; set; }
        public string NomeProfissional { get; set; }
        public int TipoProfissionalId { get; set; }
        public virtual TipoProfissional TipoProfissional { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Cpf { get; set; }
        public string INSS { get; set; }
        public DateTime DataIngresso { get; set; }
        public int Matricula { get; set; }
        public decimal TaxaBancaria { get; set; }

    }
}
