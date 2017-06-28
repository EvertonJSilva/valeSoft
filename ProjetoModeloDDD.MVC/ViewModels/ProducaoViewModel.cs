using ProjetoModeloDDD.Domain.Entities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjetoModeloDDD.MVC.ViewModels
{
    public class ProducaoViewModel
    {

        [Key]
        public int ProducaoId { get; set; }

        [DisplayName("Carteirinha")]
        public string CarteirinhaPaciente { get; set; }

        public bool revisado { get; set; }

        public bool Consolidado { get; set; }
        public DateTime dataInicial { get; set; }
        public DateTime dataFinal { get; set; }

        public int ConsultaId { get; set; }
        public virtual ConsultaViewModel Consulta { get; set; }
        
        public string DataConsulta {
            get
            {
                return Consulta.DataHoraConsulta.Date.ToString("dd/MM/yyyy");
            }
                
                }
    }
}