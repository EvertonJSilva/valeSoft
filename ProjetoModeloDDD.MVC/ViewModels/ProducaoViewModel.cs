using ProjetoModeloDDD.Domain.Entities;
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

        public int ConsultaId { get; set; }
        public virtual ConsultaViewModel Consulta { get; set; }

     

    }
}