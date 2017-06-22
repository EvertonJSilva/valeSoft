using AutoMapper;
using ProjetoModeloDDD.Application.Interface;
using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace ProjetoModeloDDD.MVC.Controllers
{
    public class PacientesController : Controller
    {
        private readonly IPacienteAppService _pacienteApp;

        public PacientesController(IPacienteAppService pacienteApp)
        {
            _pacienteApp = pacienteApp;
        }

       // GET: Paciente
        public ActionResult Index(LocalizarViewModel localizar)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("index", "login");
            }

            var pacienteViewModel = Mapper.Map<IEnumerable<Paciente>, IEnumerable<PacienteViewModel>>(_pacienteApp.GetAll());
            
            if (!String.IsNullOrEmpty(localizar.palavra))
            {
                switch (localizar.localizarPor[0])
                {
                    case "Carteira":
                        pacienteViewModel = pacienteViewModel.Where(s => s.CarteirinhaPaciente.Contains(localizar.palavra));
                        break;
                    case "Nome":
                        pacienteViewModel = pacienteViewModel.Where(s => s.NomePaciente.Contains(localizar.palavra));
                        break;
                }

            }

            LocalizarViewModel localizarViewModel = new LocalizarViewModel();
            localizarViewModel.localizarPor = new List<string>(new string[] { "Nome","Carteira" });

            var tuple = new Tuple<IEnumerable<PacienteViewModel>, LocalizarViewModel>(pacienteViewModel, localizarViewModel);

            return View(tuple);
        }

        //[HttpPost]
        public ActionResult Report(LocalizarViewModel localizar)
        {
            var pacienteViewModel = Mapper.Map<IEnumerable<Paciente>, IEnumerable<PacienteViewModel>>(_pacienteApp.GetAll());
            
            if (!String.IsNullOrEmpty(localizar.palavra))
            {
                switch (localizar.localizarPor[0])
                {
                    case "Carteira":
                        pacienteViewModel = pacienteViewModel.Where(s => s.CarteirinhaPaciente.Contains(localizar.palavra));
                        break;
                    case "Nome":
                        pacienteViewModel = pacienteViewModel.Where(s => s.NomePaciente.Contains(localizar.palavra));
                        break;
                }

            }
            
            var viewer = new Microsoft.Reporting.WebForms.ReportViewer();
            viewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"..\ProjetoModeloDDD.Infra.Data\Reports\Pacientes.rdlc";
      
            viewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("PacienteDataSet", pacienteViewModel));

            viewer.SizeToReportContent = true;
            viewer.Width = System.Web.UI.WebControls.Unit.Percentage(10);
            viewer.Height = System.Web.UI.WebControls.Unit.Percentage(10);

            ViewBag.ReportViewer = viewer;

            return View();
        }

        // GET: Paciente/Details/5
        public ActionResult Details(int id)
        {
            var paciente = _pacienteApp.GetById(id);
            var pacienteViewModel = Mapper.Map<Paciente, PacienteViewModel>(paciente);

            return View(pacienteViewModel);
        }

        // GET: Paciente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Paciente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PacienteViewModel paciente)
        {
            if (ModelState.IsValid)
            {
                var pacienteDomain = Mapper.Map<PacienteViewModel, Paciente>(paciente);
                _pacienteApp.Add(pacienteDomain);

                return RedirectToAction("Index");
            }

            return View(paciente);
        }

        // GET: Paciente/Edit/5
        public ActionResult Edit(int id)
        {
            var paciente = _pacienteApp.GetById(id);
            var pacienteViewModel = Mapper.Map<Paciente, PacienteViewModel>(paciente);

            return View(pacienteViewModel);
        }

        // POST: Paciente/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PacienteViewModel paciente)
        {
            if (ModelState.IsValid)
            {
                var pacienteDomain = Mapper.Map<PacienteViewModel, Paciente>(paciente);
                
                _pacienteApp.Update(pacienteDomain);

                return RedirectToAction("Index");
            }

            return View(paciente);
        }

        // GET: Paciente/Delete/5
        public ActionResult Delete(int id)
        {
            var paciente = _pacienteApp.GetById(id);
            var pacienteViewModel = Mapper.Map<Paciente, PacienteViewModel>(paciente);

            return View(pacienteViewModel);
        }

        // POST: Paciente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var paciente = _pacienteApp.GetById(id);
            _pacienteApp.Remove(paciente);

            return RedirectToAction("Index");
        }
    }
}
