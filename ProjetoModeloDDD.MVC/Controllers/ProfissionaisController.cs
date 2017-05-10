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
    public class ProfissionaisController : Controller
    {
        private readonly IProfissionalAppService _profissionalApp;
        

        public ProfissionaisController(IProfissionalAppService profissionalApp)
        {
            _profissionalApp = profissionalApp;
 
        }

        // GET: Consulta
        public ActionResult Index()
        {
            var profissionalViewModel = Mapper.Map<IEnumerable<Profissional>, IEnumerable<ProfissionalViewModel>>(_profissionalApp.GetAll());
            return View(profissionalViewModel);
        }

        // GET: Consulta/Details/5
        public ActionResult Details(int id)
        {
            var profissional = _profissionalApp.GetById(id);
            var profissionalViewModel = Mapper.Map<Profissional, ProfissionalViewModel>(profissional);

             return View(profissionalViewModel);
        }

        // GET: Consulta/Create
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: Consulta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProfissionalViewModel profissional)
        {
            if (ModelState.IsValid)
            {
                var profissionalDomain = Mapper.Map<ProfissionalViewModel, Profissional>(profissional);
                _profissionalApp.Add(profissionalDomain);

                return RedirectToAction("Index");
            }

            
            return View(profissional);
        }

        // GET: Consulta/Edit/5
        public ActionResult Edit(int id)
        {
            var profissional = _profissionalApp.GetById(id);
            var profissionalViewModel = Mapper.Map<Profissional, ProfissionalViewModel>(profissional);

            return View(profissionalViewModel);
        }

        // POST: Consulta/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProfissionalViewModel profissional)
        {
            if (ModelState.IsValid)
            {
                var profissionalDomain = Mapper.Map<ProfissionalViewModel, Profissional>(profissional);
                _profissionalApp.Update(profissionalDomain);

                return RedirectToAction("Index");
            }

              return View(profissional);
        }

        // GET: Consulta/Delete/5
        public ActionResult Delete(int id)
        {
            var profissional = _profissionalApp.GetById(id);
            var profissionalViewModel = Mapper.Map<Profissional, ProfissionalViewModel>(profissional);

            return View(profissionalViewModel);
        }

        // POST: Consulta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var profissional = _profissionalApp.GetById(id);
            _profissionalApp.Remove(profissional);

            return RedirectToAction("Index");
        }
    }
}
