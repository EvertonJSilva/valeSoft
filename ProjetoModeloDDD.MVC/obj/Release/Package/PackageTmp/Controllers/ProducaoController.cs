using AutoMapper;
using Microsoft.Reporting.WebForms;
using ProjetoModeloDDD.Application.Interface;
using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace ProjetoModeloDDD.MVC.Controllers
{
    public class ProducaoController : Controller
    {
        
        private readonly IProducaoAppService _producaoApp;
        private readonly IDTOProducaoAppService _DTOproducaoApp;
        private readonly ITaxaDoacaoAppService _taxaDoacaoApp;
        private readonly ITaxaExtraProfissionalAppService _taxaExtraApp;
        private readonly IDemonstrativoReportAppService _demonstrativoApp;

        public ProducaoController(IProducaoAppService producaoApp, IDTOProducaoAppService DTOproducaoApp, ITaxaDoacaoAppService taxaDoacaoApp, ITaxaExtraProfissionalAppService taxaExtraApp, IDemonstrativoReportAppService demonstrativoApp)
        {
            _producaoApp = producaoApp;
            _DTOproducaoApp = DTOproducaoApp;
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
                                    string criterioSessao,
                                    string criterioCopart,
                                    string grid1page,
                                    bool? somenteMes)
        {
            var nivelAcesso = (int)Session["nivelAcesso"];

            //default primeira pagina
            if (String.IsNullOrEmpty(grid1page))
            {
                grid1page = "1";
            }

            //não está logado
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("index", "login");
            }

            //caso não tenha passado nada traz tudo, se não for médico
            if (String.IsNullOrEmpty(dataInicial) && (somenteMes == false || nivelAcesso != 2))
            {
                dataInicial = DateTime.Now.AddDays(-15).ToString();
            }
            if (String.IsNullOrEmpty(dataFinal) && (somenteMes == false || nivelAcesso != 2))
            {
                dataFinal = DateTime.MaxValue.ToString();
            }

            // médicos mostra apenas o mês
            if ((somenteMes == true || !somenteMes.HasValue) && nivelAcesso == 2)
            {
                DateTime primeiroDia = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                dataFinal = primeiroDia.AddMonths(1).AddDays(-1).ToString();
                dataInicial = primeiroDia.ToString();
            }


            int idLocalizacao = LocalizarPor.GetValueOrDefault();
            IEnumerable<DTOProducao> listaProducao = Enumerable.Empty<DTOProducao>();

            //médicos só vê os deles
            int IdProfissional = 0 ;
            if (nivelAcesso == 2)
            {
                IdProfissional = (int)Session["idProfissional"];
            }
            
            if (!String.IsNullOrEmpty(palavra))
            {
                switch (idLocalizacao)
                {
                     //por nome
                    case 2:
                        listaProducao = _DTOproducaoApp.GetListaPorData(DateTime.Parse(dataInicial), DateTime.Parse(dataFinal), IdProfissional, palavra.ToLower());
                         break;
                    
                    //por carteira
                    case 1:
                        listaProducao = _DTOproducaoApp.GetListaPorData(DateTime.Parse(dataInicial), DateTime.Parse(dataFinal), IdProfissional, "");

                        listaProducao = listaProducao.Where(s => s.carteirinhaPaciente.ToLower().Contains(palavra.ToLower()));
                        break;

                }
            }
            else
            {
                listaProducao = _DTOproducaoApp.GetListaPorData(DateTime.Parse(dataInicial), DateTime.Parse(dataFinal), IdProfissional, "");   
            }

            
            switch (criterio)
            {
                case "todos":
                    break;
                case "nao-revisados":
                    listaProducao = listaProducao.Where(s => s.revisado == false);
                    break;
                case "revisados":
                    listaProducao = listaProducao.Where(s => s.revisado == true);
                    break;
                case "consolidados":
                    listaProducao = listaProducao.Where(s => s.consolidado == true);
                    break;
                case "nao-consolidados":
                    listaProducao = listaProducao.Where(s => s.consolidado == false);
                    break;

                default:
                    break;
            }

            switch (criterioSessao)
            {
                case "todos":
                    break;
                case "50000470":
                    listaProducao = listaProducao.Where(s => s.sessaoConsulta == "50000470");
                    break;
                case "80000509":
                    listaProducao = listaProducao.Where(s => s.sessaoConsulta == "80000509");
                    break;
                case "60000678":
                    listaProducao = listaProducao.Where(s => s.sessaoConsulta == "60000678");
                    break;
               
                default:
                    break;
            }

            switch (criterioCopart)
            {
                case "todos":
                    break;
                case "com":
                    listaProducao = listaProducao.Where(s => s.copartPaciente == true);
                    break;
                case "sem":
                    listaProducao = listaProducao.Where(s => s.copartPaciente == false);
                    break;


                default:
                    break;
            }

            //paginacao apenas nas consultas de lista
            if (acao == null)
            {
                listaProducao = Paginar(listaProducao, grid1page, 20);
                var producaoViewModel = Mapper.Map<IEnumerable<DTOProducao>, IEnumerable<DTOProducaoViewModel>>(listaProducao);

                return View(producaoViewModel);
            }
            else
            {
                var producaoViewModel = Mapper.Map<IEnumerable<DTOProducao>, IEnumerable<DTOProducaoViewModel>>(listaProducao);

                TempData["listaProducao"] = producaoViewModel;

                ///salva dados temp para poder passar paras as outras acoes
                TempData["listaEntidadeProducao"] = listaProducao;

                TempData["dataInicial"] = dataInicial;
                TempData["dataFinal"] = dataFinal;

                switch (acao)
                {
                        
                    case "REPORT":
                        return RedirectToAction("Report");

                    case "PROTOCOLO":
                        return RedirectToAction("Protocolo");

                    case "DEMONSTRATIVO":
                        return RedirectToAction("Demonstrativo");

                    case "CONSOLIDAR":

                        TimeSpan diferenca = Convert.ToDateTime(dataFinal) - Convert.ToDateTime(dataInicial);

                        if (diferenca.Days > 18)
                        {

                            ModelState.AddModelError(string.Empty, @"Impossível consolidar. O período informado superior a 1 dias.");


                            return View(producaoViewModel);
                        }

                        foreach (var producao in listaProducao)
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

        }

        public ActionResult Report()
        {
            
            var producaoViewModel = Mapper.Map<IEnumerable<DTOProducaoViewModel>, IEnumerable<DTOProducao>>(TempData["listaProducao"] as IEnumerable<DTOProducaoViewModel>);
            //var producaoViewModel =(TempData["listaEntidadeProducao"] as IEnumerable<Producao>);

            var dataInicial = Convert.ToDateTime(TempData["dataInicial"]);
            var dataFinal = Convert.ToDateTime(TempData["dataFinal"]);

            var viewer = new Microsoft.Reporting.WebForms.ReportViewer();
            //viewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
        
            #if !DEBUG
                viewer.LocalReport.ReportPath = "bin\\Reports\\Producao.rdlc";
            #else
                viewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"..\ProjetoModeloDDD.Infra.Data\Reports\Producao.rdlc";
            #endif 

            viewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("Producao", ProducaoReport.GerarLista(producaoViewModel, dataInicial, dataFinal)));

            viewer.SizeToReportContent = true;
            viewer.ShowPrintButton = true;
            viewer.ShowExportControls = true;
            viewer.ShowRefreshButton = false;

            viewer.PageCountMode = Microsoft.Reporting.WebForms.PageCountMode.Actual;
            
            viewer.Width = System.Web.UI.WebControls.Unit.Percentage(10);
            viewer.Height = 0;//System.Web.UI.WebControls.Unit.Percentage(10);

            ViewBag.ReportViewer = viewer;
            TempData["report"] = viewer;


            return View(producaoViewModel);
        }

        public ActionResult Protocolo()
        {

            var producaoViewModel = Mapper.Map<IEnumerable<DTOProducaoViewModel>, IEnumerable<DTOProducao>>(TempData["listaProducao"] as IEnumerable<DTOProducaoViewModel>);
            //var producaoViewModel = (TempData["listaEntidadeProducao"] as IEnumerable<Producao>);

            var dataInicial = Convert.ToDateTime(TempData["dataInicial"]);
            var dataFinal = Convert.ToDateTime(TempData["dataFinal"]);

            var viewer = new Microsoft.Reporting.WebForms.ReportViewer();
           // viewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            
            #if !DEBUG
                viewer.LocalReport.ReportPath = "bin\\Reports\\Protocolo.rdlc";
            #else
                viewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"..\ProjetoModeloDDD.Infra.Data\Reports\Protocolo.rdlc";
            #endif 


            //viewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("Protocolo", producaoViewModel));
            viewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("Producao", ProducaoReport.GerarLista(producaoViewModel, dataInicial, dataFinal)));

            viewer.SizeToReportContent = true;  
            viewer.Width = System.Web.UI.WebControls.Unit.Percentage(10);    
            viewer.Height = System.Web.UI.WebControls.Unit.Percentage(10);
            viewer.ShowPrintButton = true;
            viewer.ShowExportControls = true;
            viewer.ShowRefreshButton = false;

            ViewBag.ReportViewer = viewer;

            TempData["report"] = viewer;

            return View(producaoViewModel);
        }

        public ActionResult Consolidar()
        {
            var producaoViewModel = Mapper.Map<IEnumerable<DTOProducaoViewModel>, IEnumerable<DTOProducao>>(TempData["listaProducao"] as IEnumerable<DTOProducaoViewModel>);
            //var producaoViewModel = (TempData["listaEntidadeProducao"] as IEnumerable<Producao>);


            foreach (var DTOproducao in producaoViewModel)
            {
                var producao = _producaoApp.GetById(DTOproducao.idProducao);

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
            var producaoViewModel = Mapper.Map<IEnumerable<DTOProducaoViewModel>, IEnumerable<DTOProducao>>(TempData["listaProducao"] as IEnumerable<DTOProducaoViewModel>);
            //var producaoViewModel = (TempData["listaEntidadeProducao"] as IEnumerable<Producao>);

            var dataInicial = Convert.ToDateTime(TempData["dataInicial"]);
            var dataFinal = Convert.ToDateTime(TempData["dataFinal"]);

            var viewer = new Microsoft.Reporting.WebForms.ReportViewer();
            viewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                        
            #if !DEBUG
                viewer.LocalReport.ReportPath = "bin\\Reports\\Demonstrativo.rdlc";
            #else
                viewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"..\ProjetoModeloDDD.Infra.Data\Reports\Demonstrativo.rdlc";
            #endif 


            //viewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("Demonstrativo", producaoViewModel));
            viewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("Demonstrativo", _demonstrativoApp.GerarLista(producaoViewModel,_taxaDoacaoApp,_taxaExtraApp, dataInicial, dataFinal)));

            
            viewer.SizeToReportContent = true;
            viewer.Width = System.Web.UI.WebControls.Unit.Percentage(10);
            viewer.Height = System.Web.UI.WebControls.Unit.Percentage(10);
            viewer.ShowPrintButton = true;
            viewer.ShowExportControls = true;
            viewer.ShowRefreshButton = false;

            ViewBag.ReportViewer = viewer;

            TempData["report"] = viewer;

            return View(producaoViewModel);
        }

        
         public ActionResult PDFExport()
        {
            var report = TempData["report"] as Microsoft.Reporting.WebForms.ReportViewer;
            string[] streamids;
            string minetype;
            string encod;
            string fextension;
            string deviceInfo =
              "<DeviceInfo>" +
              "  <OutputFormat>EMF</OutputFormat>" +
              "  <PageWidth>8.5in</PageWidth>" +
              "  <PageHeight>11in</PageHeight>" +
              "  <MarginTop>0.25in</MarginTop>" +
              "  <MarginLeft>0.25in</MarginLeft>" +
              "  <MarginRight>0.25in</MarginRight>" +
              "  <MarginBottom>0.25in</MarginBottom>" +
              "</DeviceInfo>";
            Warning[] warnings;
            byte[] rpbybe = report.LocalReport.Render("PDF", deviceInfo, out minetype, out encod, out fextension, out streamids,
               out warnings);

            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = minetype;
            Response.AddHeader("content-disposition", "attachment; filename= filename" + "." + fextension);
            Response.OutputStream.Write(rpbybe, 0, rpbybe.Length); // create the file  
            Response.Flush(); // send it to the client to download  
            Response.End();
            //using (FileStream fs = new FileStream("E:\\newwwfg.pdf", FileMode.Create))
            //{
            //    fs.Write(rpbybe, 0, rpbybe.Length);
            //}

            var producaoViewModel = Mapper.Map<IEnumerable<ProducaoViewModel>, IEnumerable<Producao>>(TempData["listaProducao"] as IEnumerable<ProducaoViewModel>);

            return View(producaoViewModel);
        }

            public IEnumerable<DTOProducao> Paginar(IEnumerable<DTOProducao> listConsulta, String paginaAtual, int listaPorPagina)
        {

            ViewBag.TotalPage = (int) Math.Ceiling( (double) listConsulta.Count() / listaPorPagina) ;

            listConsulta = listConsulta.Skip((int.Parse(paginaAtual) - 1) * listaPorPagina);
            listConsulta = listConsulta.Take(listaPorPagina);

            ViewBag.CurrentPage = int.Parse(paginaAtual);

            if (ViewBag.CurrentPage == 1)
            {
                ViewBag.startPage = 1;
            }
            else
            {
                ViewBag.startPage = @ViewBag.CurrentPage - 1;
            }

            if ((ViewBag.CurrentPage + 5) < ViewBag.TotalPage)
            {
                ViewBag.endPage = ViewBag.CurrentPage + 5;
            }
            else
            {
                ViewBag.endPage = ViewBag.TotalPage;
            }

            return listConsulta;
        }


    }
}

