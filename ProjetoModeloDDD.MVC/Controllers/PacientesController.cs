﻿using AutoMapper;
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
    public class PacientesController : Controller
    {
        private readonly IPacienteAppService _pacienteApp;

        public PacientesController(IPacienteAppService pacienteApp)
        {
            _pacienteApp = pacienteApp;
        }

        // GET: Paciente
        public ActionResult Index()
        {
            var pacienteViewModel = Mapper.Map<IEnumerable<Paciente>, IEnumerable<PacienteViewModel>>(_pacienteApp.GetAll());
            return View(pacienteViewModel);
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