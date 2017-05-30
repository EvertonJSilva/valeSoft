using System;
using System.Collections.Generic;

namespace ProjetoModeloDDD.Domain.Entities
{
    public class Consulta
    {
        public int ConsultaId { get; set; }
        public string Autorizacao { get; set; }
     //   public string Status { get; set; }
        public string FormaAutorizar { get; set; }
        public DateTime DataHoraConsulta { get; set; }
        public decimal ValorConsulta { get; set; }
        public string Convenio { get; set; }
        public decimal ValorCopart { get; set; }
        //valor que o convenio pagará de volta a cooperativa
        public decimal ValorConvenio { get; set; }
        public string TipoSessao { get; set; }
        public DateTime DataCadastro { get; set; }
        //referencia a liberacao
        public int LiberacaoId { get; set; }
        public int ProfissionalId { get; set; }
        //referencia ao login que lançou a consulta
        public string Login { get; set; }

        public virtual Liberacao Liberacao { get; set; }
        public virtual Profissional Profissional { get; set; }
    }
}
