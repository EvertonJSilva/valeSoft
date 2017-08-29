﻿using ProjetoModeloDDD.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoModeloDDD.Domain.Entities
{
    public class DemonstrativoReport
    {

        private readonly ITaxaDoacaoRepository _taxaDoacaoRepository;

        public int Matricula { get; set; }
        public string NomeProfissional { get; set; }
        public DateTime DataIngresso { get; set; }
        public string INSS { get; set; }
        public string CPF { get; set; }
        public decimal ValorConsulta { get; set; }
        public decimal ValorCopart { get; set; }
        public decimal ValorConvenio { get; set; }
        public decimal ValorDoacao { get; set; }
        public decimal ValorOutrosDescontos { get; set; }
        public decimal ValorOutrosAcrecimos { get; set; }
        public decimal TaxaBancaria { get; set; }
        public DateTime dataInicial { get; set; }
        public DateTime dataFinal { get; set; }


        public DemonstrativoReport(Producao producao)
        {
            this.Matricula = producao.Consulta.Profissional.Matricula;
            this.NomeProfissional = producao.Consulta.Profissional.NomeProfissional;
            this.DataIngresso = producao.Consulta.Profissional.DataIngresso;
            this.INSS = producao.Consulta.Profissional.INSS;
            this.CPF = producao.Consulta.Profissional.Cpf;
            this.ValorConsulta = producao.Consulta.ValorConsulta;
            this.ValorCopart = producao.Consulta.ValorCopart;
            this.ValorConvenio = producao.Consulta.ValorConvenio;
            this.ValorOutrosDescontos = 0;
            this.ValorOutrosAcrecimos = 0;
            this.TaxaBancaria = producao.Consulta.Profissional.TaxaBancaria;
            this.dataInicial = producao.dataInicial;
            this.dataFinal = producao.dataFinal;
            
            
            try
            {
                
                var valoresTaxas = _taxaDoacaoRepository.GetPorIdTaxaProfissional(producao.Consulta.Profissional.TipoProfissionalId);
                var valor = valoresTaxas.Where(v => v.TipoProfissionalId == producao.Consulta.Profissional.TipoProfissionalId);
                this.ValorDoacao = valor.First().Valor;
            }
            catch (Exception)
            {
                this.ValorDoacao = 0;
            } 


        }

        static public List<DemonstrativoReport> GerarLista(IEnumerable<Producao> producaoLista)
        {
            List<DemonstrativoReport> lista = new List<DemonstrativoReport>();

            foreach (Producao producao in producaoLista)
            {
                lista.Add(new DemonstrativoReport(producao));
            }

            return lista;
        }  


    }
}
