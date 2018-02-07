﻿using System;
using System.Collections.Generic;
using ProjetoModeloDDD.Application.Interface;
using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Services;

namespace ProjetoModeloDDD.Application
{
    public class DemonstrativoReportAppService : IDemonstrativoReportAppService
    {
       
        public DemonstrativoReport CriarDemonstrativoReport(DTOProducao producao, ITaxaDoacaoAppService taxaDoacao, ITaxaExtraProfissionalAppService taxaExtra, DateTime dataInicial, DateTime dataFinal)
        {
            var demonstrativo = new DemonstrativoReport
            {
                Matricula = producao.matricula,
                NomeProfissional = producao.nomeProfissional,
                DataIngresso = producao.dataIngresso,
                INSS = producao.INSS,
                CPF = producao.CPF,
                ValorConsulta = producao.valorConsulta,
                ValorCopart = producao.valorCopart,
                ValorConvenio = producao.valorConvenio,
                ValorOutrosDescontos = 0,
                ValorOutrosAcrecimos = 0,
                TaxaBancaria = producao.taxaBancaria,
                dataInicial = dataInicial,
                dataFinal = dataFinal
            };


            try
            {

                TaxaDoacao valoresTaxas = taxaDoacao.GetPorIdTaxaProfissional(producao.tipoProfissionalId);
                demonstrativo.ValorDoacao = valoresTaxas.Valor;

                 IEnumerable<TaxaExtraProfissional>  valoresExtra = taxaExtra.GetPorIdTaxaExtraProfissional(producao.profissionalId);

                
                foreach (var item in valoresExtra)
                {

                    if(item.dataCompensar >= producao.dataInicial && item.dataCompensar < producao.dataFinal)
                    { 

                        if (item.tipo == "Crédito")
                        {
                            demonstrativo.ValorOutrosAcrecimos =+ item.valor;
                        }
                        else
                        {
                            demonstrativo.ValorOutrosDescontos =+ item.valor;
                        }
                    }

                }
            }
            catch (Exception e)
            {
                demonstrativo.ValorDoacao = 0;
                
            }

            return demonstrativo;
        }

        public List<DemonstrativoReport> GerarLista(IEnumerable<DTOProducao> producaoLista, ITaxaDoacaoAppService taxaDoacao, ITaxaExtraProfissionalAppService taxaExtra, DateTime dataInicial, DateTime dataFinal)
        {
            List<DemonstrativoReport> lista = new List<DemonstrativoReport>();

            foreach (DTOProducao producao in producaoLista)
            {
                lista.Add( CriarDemonstrativoReport(producao, taxaDoacao, taxaExtra, dataInicial,dataFinal));
            }

            return lista;
        }
        
    }
}
