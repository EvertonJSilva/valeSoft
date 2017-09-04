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
        private readonly ITaxaDoacaoAppService _taxaDoacaoApp;
        private readonly ITaxaExtraProfissionalAppService _taxaExtraApp;
        private readonly IDemonstrativoReportAppService _demonstrativoApp;

        public ProducaoController(IProducaoAppService producaoApp, ITaxaDoacaoAppService taxaDoacaoApp, ITaxaExtraProfissionalAppService taxaExtraApp, IDemonstrativoReportAppService demonstrativoApp)
        {
            _producaoApp = producaoApp;
            _taxaDoacaoApp = taxaDoacaoApp;
            _taxaExtraApp = taxaExtraApp;
            _demonstrativoApp = demonstrativoApp;

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


        // GET: Producao
        public ActionResult Index(string palavra,
                                    int? LocalizarPor,
                                    string dataInicial,
                                    string dataFinal,
                                    string acao,
                                    string criterio,
                                    string grid1page)
        {
            if (String.IsNullOrEmpty(grid1page))
            {
                grid1page = "1";
            }

            if (Session["Usuario"] == null)
            {
                return RedirectToAction("index", "login");
            }

            //caso não tenha passado nada traz tudo
            if (String.IsNullOrEmpty(dataInicial))
            {
                dataInicial = DateTime.Now.AddDays(-15).ToString();
            }
            if (String.IsNullOrEmpty(dataFinal))
            {
                dataFinal = DateTime.MaxValue.ToString();
            }
            
            var listaProducao = _producaoApp.GetListaPorData(DateTime.Parse(dataInicial), DateTime.Parse(dataFinal));

            int idLocalizacao = LocalizarPor.GetValueOrDefault();

            switch (criterio)
            {
                case "todos":
                    break;
                case "revisados":
                    listaProducao = listaProducao.Where(s => s.revisado == true );
                    break;
                case "nao-revisados":
                    listaProducao = listaProducao.Where(s => s.revisado == false);
                    break;
                default:
                    break;
            }

            var producaoViewModel = Mapper.Map<IEnumerable<Producao>, IEnumerable<ProducaoViewModel>>(listaProducao);

            if (!String.IsNullOrEmpty(palavra))
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

           //médicos só vê os deles
            var nivelAcesso = (int)Session["nivelAcesso"];
            if (nivelAcesso == 2)
            {
                var IdProfissional = (int)Session["idProfissional"];

                producaoViewModel = producaoViewModel.Where(s => s.Consulta.ProfissionalId == IdProfissional);
            }


            //numero de paginas 
            ViewBag.TotalPage = producaoViewModel.Count();
            int listaPorPagina = 20;

            if (acao == null)
            {
                producaoViewModel = producaoViewModel.Skip((int.Parse(grid1page) - 1) * listaPorPagina);
                producaoViewModel = producaoViewModel.Take(listaPorPagina);
            }
            ViewBag.CurrentPage = int.Parse(grid1page);


            ///salva dados temp para poder passar paras as outras acoes
            TempData["listaProducao"] = producaoViewModel;
            TempData["dataInicial"] = dataInicial;
            TempData["dataFinal"] = dataFinal;

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

                case "CONSOLIDAR":

                    TimeSpan diferenca = Convert.ToDateTime(dataFinal) - Convert.ToDateTime(dataInicial);

                    if ( diferenca.Days > 18  )
                    {
                        ModelState.AddModelError(string.Empty, @"Impossível consolidar. O período informado superior a 1 dias.");

                        return View(producaoViewModel);
                    }

                    foreach (var producao in producaoViewModel)
                    {
                        if (!producao.revisado)
                        {
                            ModelState.AddModelError(string.Empty, @"Impossível consolidar. Existem itens não revisados.");

                            return View(producaoViewModel);
                        }
                    }

                    return RedirectToAction("Consolidar");

                default:
                    return View(producaoViewModel);
            }


        }

        public ActionResult Report()
        {

            var producaoViewModel = Mapper.Map<IEnumerable<ProducaoViewModel>, IEnumerable<Producao>>(TempData["listaProducao"] as IEnumerable<ProducaoViewModel>);


            var viewer = new Microsoft.Reporting.WebForms.ReportViewer();
            //viewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
        
            #if !DEBUG
                viewer.LocalReport.ReportPath = "bin\\Reports\\Producao.rdlc";
            #else
                viewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"..\ProjetoModeloDDD.Infra.Data\Reports\Producao.rdlc";
            #endif 

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
           // viewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            
            #if !DEBUG
                viewer.LocalReport.ReportPath = "bin\\Reports\\Protocolo.rdlc";
            #else
                viewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"..\ProjetoModeloDDD.Infra.Data\Reports\Protocolo.rdlc";
            #endif 


            //viewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("Protocolo", producaoViewModel));
            viewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("Producao", ProducaoReport.GerarLista(producaoViewModel)));

            viewer.SizeToReportContent = true;  
            viewer.Width = System.Web.UI.WebControls.Unit.Percentage(10);    
            viewer.Height = System.Web.UI.WebControls.Unit.Percentage(10);

            ViewBag.ReportViewer = viewer;

            return View(producaoViewModel);
        }

        public ActionResult Consolidar()
        {
            var producaoViewModel = Mapper.Map<IEnumerable<ProducaoViewModel>, IEnumerable<Producao>>(TempData["listaProducao"] as IEnumerable<ProducaoViewModel>);


            foreach (var producao in producaoViewModel)
            {
                producao.Consolidado = true;
                producao.dataInicial = Convert.ToDateTime(TempData["dataInicial"]);
                producao.dataFinal = Convert.ToDateTime(TempData["dataFinal"]);

                _producaoApp.Update(producao);

                return View(producaoViewModel);
            }


            return View(producaoViewModel);
        }

        public ActionResult Demonstrativo()
        {
            var producaoViewModel = Mapper.Map<IEnumerable<ProducaoViewModel>, IEnumerable<Producao>>(TempData["listaProducao"] as IEnumerable<ProducaoViewModel>);

            var viewer = new Microsoft.Reporting.WebForms.ReportViewer();
            viewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                        
            #if !DEBUG
                viewer.LocalReport.ReportPath = "bin\\Reports\\Demonstrativo.rdlc";
            #else
                viewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"..\ProjetoModeloDDD.Infra.Data\Reports\Demonstrativo.rdlc";
            #endif 


            //viewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("Demonstrativo", producaoViewModel));
            viewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("Demonstrativo", _demonstrativoApp.GerarLista(producaoViewModel,_taxaDoacaoApp,_taxaExtraApp)));

            viewer.SizeToReportContent = true;
            viewer.Width = System.Web.UI.WebControls.Unit.Percentage(10);
            viewer.Height = System.Web.UI.WebControls.Unit.Percentage(10);

            ViewBag.ReportViewer = viewer;

            return View(producaoViewModel);
        }




    }
}

