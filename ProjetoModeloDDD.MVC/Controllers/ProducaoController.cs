using AutoMapper;
using ProjetoModeloDDD.Application.Interface;
using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProjetoModeloDDD.MVC.Controllers
{
    public class ProducaoController : Controller
    {
        
        private readonly IProducaoAppService _producaoApp;

        public ProducaoController(IProducaoAppService producaoApp)
        {
            _producaoApp = producaoApp;

        }
       
            
        public ActionResult Revisar(int producaoId, int cancelamento)
        {
            var producao = _producaoApp.GetById(producaoId);
            if(cancelamento == 1)
            {
                producao.revisado = false;
            }
            else
            {
                producao.revisado = true;
            }

            _producaoApp.Update(producao);

            return View();
        }


        public ActionResult Consolidar(int producaoId, int cancelamento)
        {
            var producao = _producaoApp.GetById(producaoId);
            if (cancelamento == 1)
            {
                producao.Consolidado = false;
            }
            else
            {
                producao.Consolidado = true;
            }

            _producaoApp.Update(producao);

            return View();
        }

        // GET: Producao
        public ActionResult Index(string palavra,
                                    int? LocalizarPor,
                                    string dataInicial,
                                    string dataFinal,
                                    string acao,
                                    string criterio)
        {

            if (Session["Usuario"] == null)
            {
                return RedirectToAction("index", "login");
            }

            //caso não tenha passado nada traz tudo
            if (String.IsNullOrEmpty(dataInicial))
            {
                dataInicial = DateTime.MinValue.ToString();
            }
            if (String.IsNullOrEmpty(dataFinal))
            {
                dataFinal = DateTime.MaxValue.ToString();
            }
            
            var listaProducao = _producaoApp.GetListaPorData(DateTime.Parse(dataInicial), DateTime.Parse(dataFinal));
            var producaoViewModel = Mapper.Map<IEnumerable<Producao>, IEnumerable<ProducaoViewModel>>(listaProducao);

            int idLocalizacao = LocalizarPor.GetValueOrDefault();

            switch (criterio)
            {
                case "todos":
                    break;
                case "revisados":
                    producaoViewModel = producaoViewModel.Where(s => s.revisado == true );
                    break;
                case "consolidados":
                    producaoViewModel = producaoViewModel.Where(c => c.Consolidado == true);
                    break;
                case "nao-revisados":
                    producaoViewModel = producaoViewModel.Where(s => s.revisado == false);
                    break;
                case "nao-consolidados":
                    producaoViewModel = producaoViewModel.Where(c => c.Consolidado == false);
                    break;
                default:
                    break;
            }

            if(!String.IsNullOrEmpty(palavra))
            {
                switch (idLocalizacao)
                {
                    case 2:
                        producaoViewModel = producaoViewModel.Where(s => s.Consulta.Liberacao.Paciente.NomePaciente.ToLower().Contains(palavra.ToLower()));
                        break;
                    case 1:
                        producaoViewModel = producaoViewModel.Where(s => s.CarteirinhaPaciente.ToLower().Contains(palavra.ToLower()));
                        break;     
                }
            }

            ///salva dados temp para poder passar paras as outras acoes
            TempData["listaProducao"] = producaoViewModel;

            switch (acao)
            {
                case null:
                    return View(producaoViewModel);
                    
                case "REPORT":
                    return RedirectToAction("Report");

                case "PROTOCOLO":
                    return RedirectToAction("Protocolo");

                case "DEMONSTRATIVO":
                    return RedirectToAction("Demonstrativo");

                default:
                    return View(producaoViewModel);
            }


        }

        public ActionResult Report()
        {

            var producaoViewModel = Mapper.Map<IEnumerable<ProducaoViewModel>, IEnumerable<Producao>>(TempData["listaProducao"] as IEnumerable<ProducaoViewModel>);


            var viewer = new Microsoft.Reporting.WebForms.ReportViewer();
            viewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"..\ProjetoModeloDDD.Infra.Data\Reports\Producao.rdlc";

            viewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("Producao", ProducaoReport.GerarLista(producaoViewModel)));

            viewer.SizeToReportContent = true;
            viewer.Width = System.Web.UI.WebControls.Unit.Percentage(10);
            viewer.Height = System.Web.UI.WebControls.Unit.Percentage(10);

            ViewBag.ReportViewer = viewer;

            return View(producaoViewModel);
        }

        public ActionResult Protocolo()
        {

            var producaoViewModel = Mapper.Map<IEnumerable<ProducaoViewModel>, IEnumerable<Producao>>(TempData["listaProducao"] as IEnumerable<ProducaoViewModel>);

            var viewer = new Microsoft.Reporting.WebForms.ReportViewer();
            viewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"..\ProjetoModeloDDD.Infra.Data\Reports\Protocolo.rdlc";

            //viewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("Protocolo", producaoViewModel));
            viewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("Producao", ProducaoReport.GerarLista(producaoViewModel)));

            viewer.SizeToReportContent = true;  
            viewer.Width = System.Web.UI.WebControls.Unit.Percentage(10);    
            viewer.Height = System.Web.UI.WebControls.Unit.Percentage(10);

            ViewBag.ReportViewer = viewer;

            return View(producaoViewModel);
        }

        public ActionResult Demonstrativo(string palavra, int? LocalizarPor)
        {
            var producaoViewModel = Mapper.Map<IEnumerable<ProducaoViewModel>, IEnumerable<Producao>>(TempData["listaProducao"] as IEnumerable<ProducaoViewModel>);

            var viewer = new Microsoft.Reporting.WebForms.ReportViewer();
            viewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"..\ProjetoModeloDDD.Infra.Data\Reports\Demonstrativo.rdlc";

            //viewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("Demonstrativo", producaoViewModel));
            viewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("Demonstrativo", DemonstrativoReport.GerarLista(producaoViewModel)));

            viewer.SizeToReportContent = true;
            viewer.Width = System.Web.UI.WebControls.Unit.Percentage(10);
            viewer.Height = System.Web.UI.WebControls.Unit.Percentage(10);

            ViewBag.ReportViewer = viewer;

            return View(producaoViewModel);
        }




    }
}

