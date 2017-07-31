using ProjetoModeloDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetoModeloDDD.MVC.ViewModels
{
    public class FolhaViewModel
    {
        [Key]
        public int FolhaId { get; set; }
        public DateTime Mes { get; set; }
        public DateTime Ano { get; set; }
        public decimal ValorTotal { get; set; }
        public int ProfissionalId { get; set; }
        public virtual Profissional Profissional { get; set; }


    }
}