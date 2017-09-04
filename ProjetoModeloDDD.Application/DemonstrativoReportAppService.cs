using System;
using System.Collections.Generic;
using ProjetoModeloDDD.Application.Interface;
using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Services;

namespace ProjetoModeloDDD.Application
{
    public class DemonstrativoReportAppService : IDemonstrativoReportAppService
    {
       
        public DemonstrativoReport CriarDemonstrativoReport(Producao producao, ITaxaDoacaoAppService taxaDoacao, ITaxaExtraProfissionalAppService taxaExtra)
        {
            var demonstrativo = new DemonstrativoReport
            {
                Matricula = producao.Consulta.Profissional.Matricula,
                NomeProfissional = producao.Consulta.Profissional.NomeProfissional,
                DataIngresso = producao.Consulta.Profissional.DataIngresso,
                INSS = producao.Consulta.Profissional.INSS,
                CPF = producao.Consulta.Profissional.Cpf,
                ValorConsulta = producao.Consulta.ValorConsulta,
                ValorCopart = producao.Consulta.ValorCopart,
                ValorConvenio = producao.Consulta.ValorConvenio,
                ValorOutrosDescontos = 0,
                ValorOutrosAcrecimos = 0,
                TaxaBancaria = producao.Consulta.Profissional.TaxaBancaria,
                dataInicial = producao.dataInicial,
                dataFinal = producao.dataFinal
            };


            try
            {

                TaxaDoacao valoresTaxas = taxaDoacao.GetPorIdTaxaProfissional(producao.Consulta.Profissional.TipoProfissionalId);
                demonstrativo.ValorDoacao = valoresTaxas.Valor;

                 IEnumerable<TaxaExtraProfissional>  valoresExtra = taxaExtra.GetPorIdTaxaExtraProfissional(producao.Consulta.Profissional.ProfissionalId);

                
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

        public List<DemonstrativoReport> GerarLista(IEnumerable<Producao> producaoLista, ITaxaDoacaoAppService taxaDoacao, ITaxaExtraProfissionalAppService taxaExtra)
        {
            List<DemonstrativoReport> lista = new List<DemonstrativoReport>();

            foreach (Producao producao in producaoLista)
            {
                lista.Add( CriarDemonstrativoReport(producao, taxaDoacao, taxaExtra));
            }

            return lista;
        }
        
    }
}
