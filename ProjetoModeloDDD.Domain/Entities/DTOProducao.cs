﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoModeloDDD.Domain.Entities
{
    public class DTOProducao
    {
        public string nomePaciente { get; set; }
        public string carteirinhaPaciente { get; set; }
        public string sessaoConsulta { get; set; }
        public decimal valorConsulta { get; set; }
        public DateTime dataInicial { get; set; }
        public DateTime dataFinal { get; set; }
        public int matricula { get; set; }
        public string nomeProfissional { get; set; }
        public int profissionalId { get; set;}
        public int tipoProfissionalId { get; set; }
        public DateTime dataIngresso { get; set; }
        public string INSS { get; set; }
        public string CPF { get; set; }
        public decimal valorCopart { get; set; }
        public decimal valorConvenio { get; set; }
        public decimal taxaBancaria { get; set; }
        public bool copartPaciente { get; set; }
        public bool revisado { get; set; }
        public bool consolidado { get; set; }
        public int idProducao { get; set; }
        public string autorizacao { get; set; }
        public string numeroLiberacao { get; set; }
        public DateTime dataConsulta { get; set; }
        public int pacienteId { get; set; }
        public int consultaId { get; set; }

    }

}



