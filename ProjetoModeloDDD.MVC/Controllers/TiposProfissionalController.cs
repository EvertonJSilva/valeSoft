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
    public class TiposProfissionalController : Controller
    {
        private readonly ITipoProfissionalAppService _tipoProfissionalApp;
        
        public TiposProfissionalController(ITipoProfissionalAppService tipoProfissionalApp)
        {
            _tipoProfissionalApp = tipoProfissionalApp;
        }

        // GET: Consulta
        public ActionResult Index(string palavra, int? LocalizarPor)
        {
            var tipoProfissinoalViewModel = Mapper.Map<IEnumerable<TipoProfissional>, IEnumerable<TipoProfissionalViewModel>>(_tipoProfissionalApp.GetAll());
            int idLocalizacao = LocalizarPor.GetValueOrDefault();

            if (!String.IsNullOrEmpty(palavra))
            {
                switch (idLocalizacao)
                {
                    case 1:
                        tipoProfissinoalViewModel = tipoProfissinoalViewModel.Where(s => s.Descricao.Contains(palavra));
                        break;
                    case 2:
                        tipoProfissinoalViewModel = tipoProfissinoalViewModel.Where(s => s.TipoProfissionalID.Equals(palavra));
                        break;
                }

            }

            return View(tipoProfissinoalViewModel);
        }

        // GET: Consulta/Details/5
        public ActionResult Details(int id)
        {
            var tipoProfissional = _tipoProfissionalApp.GetById(id);
            var tipoProfissionalViewModel = Mapper.Map<TipoProfissional, TipoProfissionalViewModel>(tipoProfissional);

            return View(tipoProfissionalViewModel);
        }

        // GET: Consulta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Consulta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoProfissionalViewModel tipoProfissional)
        {
            if (ModelState.IsValid)
            {
                var tipoProfissionalDomain = Mapper.Map<TipoProfissionalViewModel, TipoProfissional>(tipoProfissional);
                _tipoProfissionalApp.Add(tipoProfissionalDomain);

                return RedirectToAction("Index");
            }

            return View(tipoProfissional);
        }

        // GET: Consulta/Edit/5
        public ActionResult Edit(int id)
        {
            var tipoProfissional = _tipoProfissionalApp.GetById(id);
            var tipoProfissionalViewModel = Mapper.Map<TipoProfissional, TipoProfissionalViewModel>(tipoProfissional);

            return View(tipoProfissionalViewModel);
        }

        // POST: Consulta/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TipoProfissionalViewModel tipoProfissional)
        {
            if (ModelState.IsValid)
            {
                var tipoProfissionalDomain = Mapper.Map<TipoProfissionalViewModel, TipoProfissional>(tipoProfissional);
                _tipoProfissionalApp.Update(tipoProfissionalDomain);

                return RedirectToAction("Index");
            }

             return View(tipoProfissional);
        }

        // GET: Consulta/Delete/5
        public ActionResult Delete(int id)
        {
            var tipoProfissional = _tipoProfissionalApp.GetById(id);
            var tipoProfissionalViewModel = Mapper.Map<TipoProfissional, TipoProfissionalViewModel>(tipoProfissional);

            return View(tipoProfissionalViewModel);
        }

        // POST: Consulta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var tipoProfissionalDomain = _tipoProfissionalApp.GetById(id);
            _tipoProfissionalApp.Remove(tipoProfissionalDomain);

            return RedirectToAction("Index");
        }
    }
}