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
    public class ValoresConsultaController : Controller
    {
        private readonly IValorConsultaAppService _ValorConsultaAppService;

        public ValoresConsultaController(IValorConsultaAppService ValorConsultaAppService)
        {
            _ValorConsultaAppService = ValorConsultaAppService;
        }

        // GET: Consulta
        public ActionResult Index(string palavra, int? LocalizarPor)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("index", "login");
            }

            var valorConsultaViewModel = Mapper.Map<IEnumerable<ValorConsulta>, IEnumerable<ValorConsultaViewModel>>(_ValorConsultaAppService.GetAll());
            int idLocalizacao = LocalizarPor.GetValueOrDefault();

            if (!String.IsNullOrEmpty(palavra))
            {
                switch (idLocalizacao)
                {
                    case 1:
                        int id = Int32.Parse(palavra);
                        var valorConsulta = _ValorConsultaAppService.GetById(id);
                        var list = new List<ValorConsultaViewModel>();
                        list.Add(Mapper.Map<ValorConsulta, ValorConsultaViewModel>(valorConsulta));

                        valorConsultaViewModel = list;
                        break;
                    case 2:
                        valorConsultaViewModel = valorConsultaViewModel.Where(s => s.Sigla.Contains(palavra));
                        break;
                }

            }

            return View(valorConsultaViewModel);
        }

        // GET: Consulta/Details/5
        public ActionResult Details(int id)
        {
            var valorConsulta = _ValorConsultaAppService.GetById(id);
            var valorConsultaViewModel = Mapper.Map<ValorConsulta, ValorConsultaViewModel>(valorConsulta);

            return View(valorConsultaViewModel);
        }

        // GET: Consulta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Consulta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ValorConsultaViewModel valorConsulta)
        {
            if (ModelState.IsValid)
            {
                var valorConsultaDomain = Mapper.Map<ValorConsultaViewModel, ValorConsulta>(valorConsulta);
                _ValorConsultaAppService.Add(valorConsultaDomain);

                return RedirectToAction("Index");
            }

            return View(valorConsulta);
        }

        // GET: Consulta/Edit/5
        public ActionResult Edit(int id)
        {
            var valorConsulta = _ValorConsultaAppService.GetById(id);
            var valorConsultaViewModel = Mapper.Map<ValorConsulta, ValorConsultaViewModel>(valorConsulta);

            return View(valorConsultaViewModel);
        }

        // POST: Consulta/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ValorConsultaViewModel valorConsulta)
        {
            if (ModelState.IsValid)
            {
                var valorConsultaDomain = Mapper.Map<ValorConsultaViewModel, ValorConsulta>(valorConsulta);
                _ValorConsultaAppService.Update(valorConsultaDomain);

                return RedirectToAction("Index");
            }

            return View(valorConsulta);
        }

        // GET: Consulta/Delete/5
        public ActionResult Delete(int id)
        {
            var valorConsulta = _ValorConsultaAppService.GetById(id);
            var valorConsultaViewModel = Mapper.Map<ValorConsulta, ValorConsultaViewModel>(valorConsulta);

            return View(valorConsultaViewModel);
        }

        // POST: Consulta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var valorConsultaDomain = _ValorConsultaAppService.GetById(id);
            _ValorConsultaAppService.Remove(valorConsultaDomain);

            return RedirectToAction("Index");
        }
    }
}
