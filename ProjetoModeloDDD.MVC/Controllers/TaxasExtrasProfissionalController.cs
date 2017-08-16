using AutoMapper;
using ProjetoModeloDDD.Application;
using ProjetoModeloDDD.Application.Interface;
using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProjetoModeloDDD.MVC.Controllers
{
    public class TaxasExtrasProfissionalController : Controller
    {
        private readonly IProfissionalAppService _profissionalApp;
        private readonly ITaxaExtraProfissionalAppService _taxaExtraprofissionalAppService;

        public TaxasExtrasProfissionalController(IProfissionalAppService profissionalApp, ITaxaExtraProfissionalAppService taxaExtraProfissionalAppService)
        {
            _profissionalApp = profissionalApp;
            _taxaExtraprofissionalAppService = taxaExtraProfissionalAppService;
        }

        // GET: Consulta
        public ActionResult Index(string palavra, int? LocalizarPor)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("index", "login");
            }

            var taxaExtraProfissionalViewModel = Mapper.Map<IEnumerable<TaxaExtraProfissional>, IEnumerable<TaxaExtraProfissionalViewModel>>(_taxaExtraprofissionalAppService.GetAll());

            //int idLocalizacao = LocalizarPor.GetValueOrDefault();

            //if (!String.IsNullOrEmpty(palavra))
            //{
            //    switch (idLocalizacao)
            //    {
            //        case 1:
            //            profissionalViewModel = profissionalViewModel.Where(s => s.Cpf.Contains(palavra));
            //            break;
            //        case 2:
            //            profissionalViewModel = profissionalViewModel.Where(s => s.NomeProfissional.Contains(palavra));
            //            break;
            //    }

            //}

            return View(taxaExtraProfissionalViewModel);
        }

        // GET: Consulta/Details/5
        public ActionResult Details(int id)
        {
            var taxaExtraProfissional = _taxaExtraprofissionalAppService.GetById(id);
            var taxaExtraProfissionalViewModel = Mapper.Map<TaxaExtraProfissional,TaxaExtraProfissionalViewModel>(taxaExtraProfissional);

            return View(taxaExtraProfissionalViewModel);
        }

        // GET: Consulta/Create
        public ActionResult Create(int idProfissional)
        {
            var profissional = _profissionalApp.GetById(idProfissional);
            ViewBag.ProfissionalId = profissional.ProfissionalId;
            ViewBag.ProfissionalNome = profissional.NomeProfissional;

            return View();
        }
        
        // POST: Consulta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaxaExtraProfissionalViewModel taxa)
        {
            if (ModelState.IsValid)
            {

                var taxaextraProfissionalDomain = Mapper.Map<TaxaExtraProfissionalViewModel, TaxaExtraProfissional>(taxa);

                taxaextraProfissionalDomain.dataInsercao = DateTime.Now;
                _taxaExtraprofissionalAppService.Add(taxaextraProfissionalDomain);

                return RedirectToAction("Details","Profissionais",new { id = taxaextraProfissionalDomain.ProfissionalId });
            }
            //ViewBag.TipoProfissionalId = new SelectList(_tipoProfissionalAppService.GetAll(), "TipoProfissionalId", "Descricao", profissional.TipoProfissionalId);

            return View(taxa);
        }

        // GET: Consulta/Edit/5
        public ActionResult Edit(int id)
        {
            var taxaExtraProfissional = _taxaExtraprofissionalAppService.GetById(id);
            var taxaExtraProfissionalViewModel = Mapper.Map<TaxaExtraProfissional, TaxaExtraProfissionalViewModel>(taxaExtraProfissional);

          //  ViewBag.TipoProfissionalId = new SelectList(_tipoProfissionalAppService.GetAll(), "TipoProfissionalId", "Descricao", profissionalViewModel.TipoProfissionalId);

            return View(taxaExtraProfissionalViewModel);
        }

        // POST: Consulta/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaxaExtraProfissionalViewModel taxaExtra)
        {
            if (ModelState.IsValid)
            {
                var taxaExtraProfissionalDomain = Mapper.Map<TaxaExtraProfissionalViewModel, TaxaExtraProfissional>(taxaExtra);
                _taxaExtraprofissionalAppService.Update(taxaExtraProfissionalDomain);

                return RedirectToAction("Index");
            }

           
            return View(taxaExtra);
        }

        // GET: Consulta/Delete/5
        public ActionResult Delete(int id)
        {
            var taxaExtraProfissional = _taxaExtraprofissionalAppService.GetById(id);
            var taxaExtraProfissionalViewModel = Mapper.Map<TaxaExtraProfissional, TaxaExtraProfissionalViewModel>(taxaExtraProfissional);

            return View(taxaExtraProfissionalViewModel);
        }

        // POST: Consulta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var taxaExtraProfissional = _taxaExtraprofissionalAppService.GetById(id);
            _taxaExtraprofissionalAppService.Remove(taxaExtraProfissional);

            return RedirectToAction("Index");
        }
    }
}
