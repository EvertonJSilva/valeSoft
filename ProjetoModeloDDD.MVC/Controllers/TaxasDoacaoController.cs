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
    public class TaxasDoacaoController : Controller
    {
        private readonly ITaxaDoacaoAppService _TaxaDoacaoAppService;
        private readonly ITipoProfissionalAppService _TipoProfissionalAppService;


        public TaxasDoacaoController(ITaxaDoacaoAppService TaxaDoacaoAppService, ITipoProfissionalAppService TipoProfissionalAppService)
        {
            _TaxaDoacaoAppService = TaxaDoacaoAppService;
            _TipoProfissionalAppService = TipoProfissionalAppService;
        }

        // GET: Consulta
        public ActionResult Index(string palavra, int? LocalizarPor)
        {
            var taxaDoacaoViewModel = Mapper.Map<IEnumerable<TaxaDoacao>, IEnumerable<TaxaDoacaoViewModel>>(_TaxaDoacaoAppService.GetAll());
            int idLocalizacao = LocalizarPor.GetValueOrDefault();

            if (!String.IsNullOrEmpty(palavra))
            {
                switch (idLocalizacao)
                {
                    case 1:

                        int id = Int32.Parse(palavra);
                        var taxaDoacao = _TaxaDoacaoAppService.GetById(id);
                        var list = new List<TaxaDoacaoViewModel>();
                        list.Add(Mapper.Map<TaxaDoacao, TaxaDoacaoViewModel>(taxaDoacao));

                        taxaDoacaoViewModel = list;
                        break;
                    case 2:
                        taxaDoacaoViewModel = taxaDoacaoViewModel.Where(s => s.TipoProfissional.Descricao.Contains(palavra));
                        break;
                }

            }

            return View(taxaDoacaoViewModel);
        }

        // GET: Consulta/Details/5
        public ActionResult Details(int id)
        {
            var taxaDoacao = _TaxaDoacaoAppService.GetById(id);
            var taxaDoacaoViewModel = Mapper.Map<TaxaDoacao, TaxaDoacaoViewModel>(taxaDoacao);

            ViewBag.TipoProfissionalId = new SelectList(_TipoProfissionalAppService.GetAll(), "TipoProfissionalId", "Descricao", taxaDoacaoViewModel.TipoProfissionalId);

            return View(taxaDoacaoViewModel);
        }

        // GET: Consulta/Create
        public ActionResult Create()
        {
            ViewBag.TipoProfissionalId = new SelectList(_TipoProfissionalAppService.GetAll(), "TipoProfissionalId", "Descricao");

            return View();
        }

        // POST: Consulta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaxaDoacaoViewModel taxaDoacao)
        {
            if (ModelState.IsValid)
            {
                var taxaDoacaoDomain = Mapper.Map<TaxaDoacaoViewModel, TaxaDoacao>(taxaDoacao);
                _TaxaDoacaoAppService.Add(taxaDoacaoDomain);

                return RedirectToAction("Index");
            }

            ViewBag.TipoProfissionalId = new SelectList(_TipoProfissionalAppService.GetAll(), "TipoProfissionalId", "Descricao", taxaDoacao.TipoProfissionalId);

            return View(taxaDoacao);
        }

        // GET: Consulta/Edit/5
        public ActionResult Edit(int id)
        {
            var taxaDoacao = _TaxaDoacaoAppService.GetById(id);
            var taxaDoacaoViewModel = Mapper.Map<TaxaDoacao, TaxaDoacaoViewModel>(taxaDoacao);

            ViewBag.TipoProfissionalId = new SelectList(_TipoProfissionalAppService.GetAll(), "TipoProfissionalId", "Descricao", taxaDoacaoViewModel.TipoProfissionalId);

            return View(taxaDoacaoViewModel);
        }

        // POST: Consulta/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaxaDoacaoViewModel taxaDoacao)
        {
            if (ModelState.IsValid)
            {
                var taxaDoacaoDomain = Mapper.Map<TaxaDoacaoViewModel, TaxaDoacao>(taxaDoacao);
                _TaxaDoacaoAppService.Update(taxaDoacaoDomain);

                return RedirectToAction("Index");
            }

            return View(taxaDoacao);
        }

        // GET: Consulta/Delete/5
        public ActionResult Delete(int id)
        {
            var taxaDoacao = _TaxaDoacaoAppService.GetById(id);
            var taxaDoacaoViewModel = Mapper.Map<TaxaDoacao, TaxaDoacaoViewModel>(taxaDoacao);

            return View(taxaDoacaoViewModel);
        }

        // POST: Consulta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var taxaDoacaoDomain = _TaxaDoacaoAppService.GetById(id);
            _TaxaDoacaoAppService.Remove(taxaDoacaoDomain);

            return RedirectToAction("Index");
        }
    }
}