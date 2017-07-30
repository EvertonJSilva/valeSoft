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
    public class ConsultasController : Controller
    {

        private readonly IConsultaAppService _consultaApp;
        private readonly ILiberacaoAppService _liberacaoApp;
        private readonly IProfissionalAppService _profissionalApp;
        private readonly IProducaoAppService _producaoApp;
        private readonly IValorConsultaAppService _valorApp;

        public ConsultasController(IConsultaAppService consultaApp,
                                ILiberacaoAppService liberacaoApp,
                                IProfissionalAppService profissionalApp,
                                IProducaoAppService producaoApp,
                                IValorConsultaAppService valorApp)
        {
            _consultaApp = consultaApp;
            _liberacaoApp = liberacaoApp;
            _profissionalApp = profissionalApp;
            _producaoApp = producaoApp;
            _valorApp = valorApp;
        }

        // GET: Consulta
        public ActionResult Index(string palavra, int? LocalizarPor)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("index", "login");
            }


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

            // médicos só vem as consultas dele
            var nivelAcesso = (int)Session["nivelAcesso"];
            if (nivelAcesso == 2)
            {
                var IdProfissional = (int)Session["idProfissional"];

                consultaViewModel = consultaViewModel.Where(s => s.Profissional.ProfissionalId == IdProfissional);
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

        public ActionResult ListaValores(string sessao, string liberacaoid)
        {
            var liberacao = _liberacaoApp.GetById(Int32.Parse(liberacaoid));

            var valoresConsultas = ListaValoresConsultas(liberacao.Paciente.CarteirinhaPaciente.Substring(0,4), sessao);

            var valor = valoresConsultas.FirstOrDefault(c => c.Selected);
            
            return Json(valor, JsonRequestBehavior.AllowGet);
        }

        public IEnumerable<ListaValores> ListaValoresConsultas(string sigla, string sessao)
        {
            IEnumerable<ListaValores> selectListvalores =
                          from c in _valorApp.GetPorSigla(sigla)
                          select new ListaValores(c)
                          {
                              Selected = (c.Sessao == sessao)
                          };

            return selectListvalores;

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
                consultaDomain.DataCadastro = DateTime.Now;
                _consultaApp.Add(consultaDomain);

                return RedirectToAction("Index");
            }

            ViewBag.LiberacaoId = listaLiberacao(consulta);
            ViewBag.ProfissionalId = listaProfissional(consulta);

            return View(consulta);
        }

        // GET: Consulta/Edit/5
        public ActionResult Edit(int id, int idLiberacao)
        {
            var consulta = _consultaApp.GetById(id);
            var consultaViewModel = Mapper.Map<Consulta, ConsultaViewModel>(consulta);

            ViewBag.idLiberacaoOrigem = idLiberacao;

            ViewBag.LiberacaoId = listaLiberacao(consultaViewModel);
            ViewBag.ProfissionalId = listaProfissional(consultaViewModel);

            return View(consultaViewModel);
        }

        // POST: Consulta/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ConsultaViewModel consulta, int idLiberacaoOrigem)
        {
            ViewBag.idLiberacaoOrigem = idLiberacaoOrigem;
            ViewBag.LiberacaoId = listaLiberacao(consulta);
            ViewBag.ProfissionalId = listaProfissional(consulta);
            
            if (consulta.ProfissionalId == 1)
            {
                ModelState.AddModelError(string.Empty, @"Profissional não selecionado");

                return View(consulta);
            }

            if (consulta.DataHoraConsulta.Year == 1)
            {
                ModelState.AddModelError(string.Empty, @"Data selecionada inválida");

                return View(consulta);
            }

            if (consulta.DataHoraConsulta < DateTime.Now)
            {
                ModelState.AddModelError(string.Empty, @"Data da consulta deve maior que hoje.");

                return View(consulta);
            }


            if (ModelState.IsValid)
            {
                var consultaDomain = Mapper.Map<ConsultaViewModel, Consulta>(consulta);
                consultaDomain.Status = "Agendado";

                _consultaApp.Update(consultaDomain);

                //inserir na produção
                try
                {
                    _producaoApp.GetPorConsultaID(consultaDomain.ConsultaId);
                }
                catch (Exception e)
                {
                    var producaoDomain = new Producao();
                    var liberacaoDomain = new Liberacao();

                    liberacaoDomain = _liberacaoApp.GetById(consultaDomain.LiberacaoId);

                    producaoDomain.revisado = false;
                    producaoDomain.ConsultaId = consultaDomain.ConsultaId;
                    producaoDomain.CarteirinhaPaciente = liberacaoDomain.Paciente.CarteirinhaPaciente;

                    _producaoApp.Add(producaoDomain);
                }

                ///redireciona para a liberacao
                if (idLiberacaoOrigem > 0) {
                    return RedirectToAction("Details", "Liberacoes", new { id = idLiberacaoOrigem });
                } else
                {
                    return RedirectToAction("Index");
                }
            }

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

        public IEnumerable<SelectListItem> listaProfissional(ConsultaViewModel consulta)
        {
            IEnumerable<SelectListItem> selectListProfissional =
               from c in _profissionalApp.GetAll()
               select new SelectListItem
               {
                   Selected = (c.ProfissionalId == consulta.ProfissionalId),
                   Text = c.NomeProfissional,
                   Value = c.ProfissionalId.ToString()
               };

            return selectListProfissional;
        }

        public IEnumerable<SelectListItem> listaLiberacao(ConsultaViewModel consulta)
        {
            IEnumerable<SelectListItem> selectListLiberacao =
                from c in _liberacaoApp.GetAll()
                select new SelectListItem
                {
                    Selected = (c.LiberacaoId == consulta.LiberacaoId),
                    Text = c.NumeroLiberacao,
                    Value = c.LiberacaoId.ToString()
                };

            return selectListLiberacao;
        }

    }


    public class ListaValores : SelectListItem
    {

        public decimal _ValorConsulta { get; set; }
        public decimal _ValorCopart { get; set; }
        public decimal _ValorConvenio { get; set; }

        public ListaValores(ValorConsulta valorConsulta)
        {
            this.Value = valorConsulta.Sessao;
            this.Text = calculaTexto(valorConsulta.Sessao);
            this._ValorConsulta = valorConsulta.Valor;
            this._ValorCopart = CalculaCopart(valorConsulta);
            this._ValorConvenio = CalculaConvenio(valorConsulta);
        }

        private decimal CalculaCopart(ValorConsulta valorConsulta)
        {
            switch (valorConsulta.TemCopart)
            {
                case true:
                    return 15;
                case false:
                    return 0;
                default:
                    return 0;
            }
        }

        private decimal CalculaConvenio(ValorConsulta valorConsulta)
        {
            switch (valorConsulta.TemCopart)
            {
                case true:
                    return valorConsulta.Valor - 15;
                case false:
                    return valorConsulta.Valor;
                default:
                    return 0;
            }
        }

        private string calculaTexto(string sessao)
        {
            switch (sessao) {
                case "50000470":
                    return "Comparecimento";
                case "80000509":
                    return "Não-Comparecimento";
                case "60000678":
                    return "Psico-Social";
                default:
                    return "";
            }
        }
    }
}