using ProjetoModeloDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetoModeloDDD.MVC.ViewModels
{
    public class FolhaDetalheViewModel
    {
        [Key]
        public int FolhaDetalheId { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int FolhaId { get; set; }
        public virtual Folha Folha { get; set; }
    }
}