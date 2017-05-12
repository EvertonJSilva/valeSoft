using AutoMapper;
using ProjetoModeloDDD.Application.Interface;
using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoModeloDDD.MVC.Controllers
{
    public class ConsultasController : Controller
    {

        private readonly IConsultaAppService _consultaApp;
        private readonly ILiberacaoAppService _liberacaoApp;
        private readonly IProfissionalAppService _profissionalApp;


        public ConsultasController (IConsultaAppService consultaApp, ILiberacaoAppService liberacaoApp, IProfissionalAppService profissionalApp)
        {
            _consultaApp = consultaApp;
            _liberacaoApp = liberacaoApp;
            _profissionalApp = profissionalApp;

        }
        
        // GET: Consulta
        public ActionResult Index(string palavra, int? LocalizarPor)
        {
            var consultaViewModel = Mapper.Map<IEnumerable<Consulta>, IEnumerable<ConsultaViewModel>>(_consultaApp.GetAll());

            int idLocalizacao = LocalizarPor.GetValueOrDefault();

            if (!String.IsNullOrEmpty(palavra))
            {
                switch (idLocalizacao)
                {
                    case 2:
                        consultaViewModel = consultaViewModel.Where(s => s.Liberacao.Paciente.NomePaciente.Contains(palavra));
                        break;
                    case 1:
                        consultaViewModel = consultaViewModel.Where(s => s.Liberacao.NumeroLiberacao.Contains(palavra));
                        break;
                }

            }
            return View(consultaViewModel);
        }

        // GET: Consulta/Details/5
        public ActionResult Details(int id)
        {
            var consulta = _consultaApp.GetById(id);
            var consultaViewModel = Mapper.Map<Consulta, ConsultaViewModel>(consulta);

            ViewBag.LiberacaoId = new SelectList(_liberacaoApp.GetAll(), "LiberacaoId", "NumeroLiberacao");
            ViewBag.ProfissionalId = new SelectList(_profissionalApp.GetAll(), "ProfissionalId", "NomeProfissional");

            return View(consultaViewModel);
        }

        // GET: Consulta/Create
        public ActionResult Create()
        {
            ViewBag.LiberacaoId = new SelectList(_liberacaoApp.GetAll(), "LiberacaoId", "NumeroLiberacao");
            ViewBag.ProfissionalId = new SelectList(_profissionalApp.GetAll(), "ProfissionalId", "NomeProfissional");

            return View();
        }

        // POST: Consulta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ConsultaViewModel consulta)
        {
            if (ModelState.IsValid)
            {
                var consultaDomain = Mapper.Map<ConsultaViewModel, Consulta>(consulta);
                _consultaApp.Add(consultaDomain);

                return RedirectToAction("Index");
            }

            ViewBag.LiberacaoId = new SelectList(_liberacaoApp.GetAll(), "LiberacaoId", "NumeroLiberacao", consulta.LiberacaoId);
            ViewBag.ProfissionalId = new SelectList(_profissionalApp.GetAll(), "ProfissionalId", "NomeProfissional", consulta.ProfissionalId);

            return View(consulta);
        }

        // GET: Consulta/Edit/5
        public ActionResult Edit(int id)
        {
            var consulta = _consultaApp.GetById(id);
            var consultaViewModel = Mapper.Map<Consulta, ConsultaViewModel>(consulta);


            ViewBag.LiberacaoId = new SelectList(_liberacaoApp.GetAll(), "LiberacaoId", "NumeroLiberacao", consulta.LiberacaoId);
            ViewBag.ProfissionalId = new SelectList(_profissionalApp.GetAll(), "ProfissionalId", "NomeProfissional", consulta.ProfissionalId);
            return View(consultaViewModel);
        }

        // POST: Consulta/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ConsultaViewModel consulta)
        {
            if (ModelState.IsValid)
            {
                var consultaDomain = Mapper.Map<ConsultaViewModel, Consulta>(consulta);
                _consultaApp.Update(consultaDomain);

                return RedirectToAction("Index");
            }


            ViewBag.LiberacaoId = new SelectList(_liberacaoApp.GetAll(), "LiberacaoId", "NumeroLiberacao", consulta.LiberacaoId);
            ViewBag.ProfissionalId = new SelectList(_profissionalApp.GetAll(), "ProfissionalId", "NomeProfissional", consulta.ProfissionalId);
            return View(consulta);
        }

        // GET: Consulta/Delete/5
        public ActionResult Delete(int id)
        {
            var consulta = _consultaApp.GetById(id);
            var consultaViewModel = Mapper.Map<Consulta, ConsultaViewModel>(consulta);

            return View(consultaViewModel);
        }

        // POST: Consulta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var consulta = _consultaApp.GetById(id);
            _consultaApp.Remove(consulta);

            return RedirectToAction("Index");
        }
    }
}
