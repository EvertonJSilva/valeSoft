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
            
            
            int idLocalizacao = LocalizarPor.GetValueOrDefault();
            IEnumerable<Producao> listaProducao = Enumerable.Empty<Producao>();

            //médicos só vê os deles
            var nivelAcesso = (int)Session["nivelAcesso"];
            int IdProfissional = 0 ;
            if (nivelAcesso == 2)
            {
                IdProfissional = (int)Session["idProfissional"];

                //producaoViewModel = producaoViewModel.Where(s => s.Consulta.ProfissionalId == IdProfissional);
            }
            
            if (!String.IsNullOrEmpty(palavra))
            {
                switch (idLocalizacao)
                {
                    case 2:
                        listaProducao = _producaoApp.GetListaPorData(DateTime.Parse(dataInicial), DateTime.Parse(dataFinal), IdProfissional, palavra.ToLower());

                     //   producaoViewModel = producaoViewModel.Where(s => s.Consulta.Liberacao.Paciente.NomePaciente.ToLower().Contains(palavra.ToLower()));
                        break;
                    case 1:
                        listaProducao = _producaoApp.GetListaPorData(DateTime.Parse(dataInicial), DateTime.Parse(dataFinal), IdProfissional, "");

                        listaProducao = listaProducao.Where(s => s.CarteirinhaPaciente.ToLower().Contains(palavra.ToLower()));
                        break;

                }
            }
            else
            {
                listaProducao = _producaoApp.GetListaPorData(DateTime.Parse(dataInicial), DateTime.Parse(dataFinal), IdProfissional, "");   
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
                    listaProducao = listaProducao.Where(s => s.Consolidado == true);
                    break;
                case "nao-consolidados":
                    listaProducao = listaProducao.Where(s => s.Consolidado == false);
                    break;

                default:
                    break;
            }


            //paginacao apenas nas consultas de lista
            if (acao == null)
            {
                listaProducao = Paginar(listaProducao, grid1page, 20);
            }
            

            var producaoViewModel = Mapper.Map<IEnumerable<Producao>, IEnumerable<ProducaoViewModel>>(listaProducao);

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
            TempData["report"] = viewer;


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

            TempData["report"] = viewer;

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

            //Warning[] warnings;
            //string[] streamIds;
            //string mimeType = string.Empty;
            //string encoding = "Processando";
            //string extension = string.Empty;

            //byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            //// byte[] bytes = viewer.LocalReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            //// Now that you have all the bytes representing the PDF report, buffer it and send it to the client.          
            //// System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.Buffer = true;
            //Response.Clear();
            //Response.ContentType = mimeType;
            //Response.AddHeader("content-disposition", "attachment; filename= filename" + "." + extension);
            //Response.OutputStream.Write(bytes, 0, bytes.Length); // create the file  
            //Response.Flush(); // send it to the client to download  
            //Response.End();

            viewer.SizeToReportContent = true;
            viewer.Width = System.Web.UI.WebControls.Unit.Percentage(10);
            viewer.Height = System.Web.UI.WebControls.Unit.Percentage(10);

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

            public IEnumerable<Producao> Paginar(IEnumerable<Producao> listConsulta, String paginaAtual, int listaPorPagina)
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

