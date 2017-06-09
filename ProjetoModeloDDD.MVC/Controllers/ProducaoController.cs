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



        // GET: Producao
        //public ActionResult Index(string palavra, int? LocalizarPor)
        //{

        //    var producaoViewModel = Mapper.Map<IEnumerable<Producao>, IEnumerable<ProducaoViewModel>>(_producaoApp.GetAll());
        //    int idLocalizacao = LocalizarPor.GetValueOrDefault();

        //    if(!String.IsNullOrEmpty(palavra))
        //    {
        //        switch(idLocalizacao)
        //        {
        //            case 2:
        //                producaoViewModel = producaoViewModel.Where(p => p.NomePaciente.Contains(palavra));
        //                break;
        //            case 1:
        //                producaoViewModel = producaoViewModel.Where(p => p.CarteirinhaPaciente.Contains(palavra));
        //                break;
        //        }
        //    }

        //    return View(producaoViewModel);

        //}

            
        public ActionResult Revisar(int producaoId)
        {
            var producao = _producaoApp.GetById(producaoId);
            producao.revisado = true;

            _producaoApp.Update(producao);

            return View();
        }


        // GET: Producao
        public ActionResult Index(string palavra, int? LocalizarPor)
        {

            var producaoViewModel = Mapper.Map<IEnumerable<Producao>, IEnumerable<ProducaoViewModel>>(_producaoApp.GetAll());

            int idLocalizacao = LocalizarPor.GetValueOrDefault();

            if(!String.IsNullOrEmpty(palavra))
            {
                switch (idLocalizacao)
                {
                    case 2:
                        producaoViewModel = producaoViewModel.Where(s => s.Consulta.Liberacao.Paciente.NomePaciente.Contains(palavra));
                        break;
                    case 1:
                        producaoViewModel = producaoViewModel.Where(s => s.CarteirinhaPaciente.Contains(palavra));
                        break;     
                }
            }

            return View(producaoViewModel);

        }

        public ActionResult Report(string palavra, int? LocalizarPor)
        {
//            var producaoViewModel = Mapper.Map<IEnumerable<Producao>, IEnumerable<ProducaoViewModel>>(_producaoApp.GetAll());

            var producaoViewModel = _producaoApp.GetAll();

            int idLocalizacao = LocalizarPor.GetValueOrDefault();

            if (!String.IsNullOrEmpty(palavra))
            {
                switch (idLocalizacao)
                {
                    case 2:
                        producaoViewModel = producaoViewModel.Where(s => s.Consulta.Liberacao.Paciente.NomePaciente.Contains(palavra));
                        break;
                    case 1:
                        producaoViewModel = producaoViewModel.Where(s => s.CarteirinhaPaciente.Contains(palavra));
                        break;
                }
            }

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

        public ActionResult Protocolo(string palavra, int? LocalizarPor)
        {
            //var producaoViewModel = Mapper.Map<IEnumerable<Producao>, IEnumerable<ProducaoViewModel>>(_producaoApp.GetAll());

            var producaoViewModel = _producaoApp.GetAll();

            int idLocalizacao = LocalizarPor.GetValueOrDefault();


            if (!String.IsNullOrEmpty(palavra))
            {
                switch (idLocalizacao)
                {
                    case 2:
                        producaoViewModel = producaoViewModel.Where(s => s.Consulta.Liberacao.Paciente.NomePaciente.Contains(palavra));
                        break;
                    case 1:
                        producaoViewModel = producaoViewModel.Where(s => s.CarteirinhaPaciente.Contains(palavra));
                        break;
                }
            }

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
            var producaoViewModel = Mapper.Map<IEnumerable<Producao>, IEnumerable<ProducaoViewModel>>(_producaoApp.GetAll());

            int idLocalizacao = LocalizarPor.GetValueOrDefault();

            if (!String.IsNullOrEmpty(palavra))
            {
                switch (idLocalizacao)
                {
                    case 2:
                        producaoViewModel = producaoViewModel.Where(s => s.Consulta.Liberacao.Paciente.NomePaciente.Contains(palavra));
                        break;
                    case 1:
                        producaoViewModel = producaoViewModel.Where(s => s.CarteirinhaPaciente.Contains(palavra));
                        break;
                }
            }

            var viewer = new Microsoft.Reporting.WebForms.ReportViewer();
            viewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"..\ProjetoModeloDDD.Infra.Data\Reports\Demonstrativo.rdlc";

            viewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("Demonstrativo", producaoViewModel));

            viewer.SizeToReportContent = true;
            viewer.Width = System.Web.UI.WebControls.Unit.Percentage(10);
            viewer.Height = System.Web.UI.WebControls.Unit.Percentage(10);

            ViewBag.ReportViewer = viewer;

            return View(producaoViewModel);
        }




    }
}

