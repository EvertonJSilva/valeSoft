using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjetoModeloDDD.MVC.ViewModels
{
    public class ProducaoViewModel
    {

        [Key]
        public int ProducaoId { get; set; }

        [DisplayName("Nome")]
        public string NomePaciente { get; set; }
        [DisplayName("Carteirinha")]
        public string CarteirinhaPaciente { get; set; }
        [DisplayName("Sessão")]
        public string Sessao { get; set; }
        [DisplayName("ValorConvenio")] 
        public decimal ValorConvenio { get; set; }




    }
}