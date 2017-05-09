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
    public class LiberacoesController : Controller
    {
        private readonly ILiberacaoAppService _liberacaoApp;
        private readonly IPacienteAppService _pacienteApp;


        public LiberacoesController(ILiberacaoAppService liberacaoApp, IPacienteAppService pacienteApp)
        {
            _liberacaoApp = liberacaoApp;
            _pacienteApp = pacienteApp;
        }

        // GET: Consulta
        public ActionResult Index()
        {
            var liberacaoViewModel = Mapper.Map<IEnumerable<Liberacao>, IEnumerable<LiberacaoViewModel>>(_liberacaoApp.GetAll());
            return View(liberacaoViewModel);
        }

        // GET: Consulta/Details/5
        public ActionResult Details(int id)
        {
            var liberacao = _liberacaoApp.GetById(id);
            var liberacaoViewModel = Mapper.Map<Liberacao, LiberacaoViewModel>(liberacao);


            ViewBag.PacienteId = new SelectList(_pacienteApp.GetAll(), "PacienteId", "NomePaciente", liberacao.PacienteId);
            return View(liberacaoViewModel);
        }

        // GET: Consulta/Create
        public ActionResult Create()
        {
            ViewBag.PacienteId = new SelectList(_pacienteApp.GetAll(), "PacienteId", "NomePaciente");

            return View();
        }

        // POST: Consulta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LiberacaoViewModel liberacao)
        {
            if (ModelState.IsValid)
            {
                var liberacaoDomain = Mapper.Map<LiberacaoViewModel, Liberacao>(liberacao);
                _liberacaoApp.Add(liberacaoDomain);

                return RedirectToAction("Index");
            }

            ViewBag.PacienteId = new SelectList(_pacienteApp.GetAll(), "PacienteId", "NomePaciente", liberacao.PacienteId);

            return View(liberacao);
        }

        // GET: Consulta/Edit/5
        public ActionResult Edit(int id)
        {
            var liberacao = _liberacaoApp.GetById(id);
            var liberacaoViewModel = Mapper.Map<Liberacao, LiberacaoViewModel>(liberacao);

            ViewBag.PacienteId = new SelectList(_pacienteApp.GetAll(), "PacienteId", "NomePaciente", liberacao.PacienteId);
            return View(liberacaoViewModel);
        }

        // POST: Consulta/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LiberacaoViewModel liberacao)
        {
            if (ModelState.IsValid)
            {
                var liberacaoDomain = Mapper.Map<LiberacaoViewModel, Liberacao>(liberacao);
                _liberacaoApp.Update(liberacaoDomain);

                return RedirectToAction("Index");
            }


            ViewBag.PacienteId = new SelectList(_pacienteApp.GetAll(), "PacienteId", "NomePaciente", liberacao.PacienteId);
            return View(liberacao);
        }

        // GET: Consulta/Delete/5
        public ActionResult Delete(int id)
        {
            var liberacao = _liberacaoApp.GetById(id);
            var liberacaoViewModel = Mapper.Map<Liberacao, LiberacaoViewModel>(liberacao);

            return View(liberacaoViewModel);
        }

        // POST: Consulta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var liberacao = _liberacaoApp.GetById(id);
            _liberacaoApp.Remove(liberacao);

            return RedirectToAction("Index");
        }
    }
}
