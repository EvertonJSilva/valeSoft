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
        private readonly IConsultaAppService _consultaApp;
        private readonly IProfissionalAppService _profissionalApp;

        public LiberacoesController(ILiberacaoAppService liberacaoApp,
                                    IPacienteAppService pacienteApp, 
                                    IConsultaAppService consultaApp,
                                    IProfissionalAppService profissionalApp)
        {
            _liberacaoApp = liberacaoApp;
            _pacienteApp = pacienteApp;
            _consultaApp = consultaApp;
            _profissionalApp = profissionalApp;
        }

        // GET: Consulta
        public ActionResult Index(string palavra, int? LocalizarPor, string grid1page)
        {
            IEnumerable<Liberacao> listaLiberacao = Enumerable.Empty<Liberacao>();

            if (Session["Usuario"] == null)
            {
                return RedirectToAction("index", "login");
            }

            int idLocalizacao = LocalizarPor.GetValueOrDefault();
            int IdProfissional = 0;
            var nivelAcesso = (int)Session["nivelAcesso"];
            if (nivelAcesso == 2)
            {
                IdProfissional = (int)Session["idProfissional"];
            }
            
            if (!String.IsNullOrEmpty(palavra))
            {

                switch (idLocalizacao)
                {
                    case 1:
                        listaLiberacao = _liberacaoApp.GetPorIdProfissional(IdProfissional, "", palavra);

                        break;
                    case 2:
                        listaLiberacao = _liberacaoApp.GetPorIdProfissional(IdProfissional, palavra.ToLower(), "");
                        break;
                }

            }
            else
            {
                listaLiberacao = _liberacaoApp.GetPorIdProfissional(IdProfissional, "", "");
            }

            listaLiberacao = Paginar(listaLiberacao, grid1page, 20);

            var liberacaoViewModel = Mapper.Map<IEnumerable<Liberacao>, IEnumerable<LiberacaoViewModel>>(listaLiberacao);

            return View(liberacaoViewModel.OrderBy(p => p.NumeroLiberacao).OrderBy(p => p.Paciente.NomePaciente));
        }

        // GET: Consulta/Details/5
        public ActionResult Details(int id)
        {
            var liberacao = _liberacaoApp.GetById(id);
            var liberacaoViewModel = Mapper.Map<Liberacao, LiberacaoViewModel>(liberacao);

            var consultasViewModel = Mapper.Map<IEnumerable<Consulta>, IEnumerable<ConsultaViewModel>>(_consultaApp.GetAll());

            consultasViewModel = consultasViewModel.Where(s => s.LiberacaoId == id);

            var tuple = new Tuple<LiberacaoViewModel, IEnumerable<ConsultaViewModel>>(liberacaoViewModel, consultasViewModel);

            return View(tuple);
        }

        // GET: Consulta/Create
        public ActionResult Create()
        {

            ViewBag.PacienteId = listaPaciente(1);

            var nivelAcesso = (int)Session["nivelAcesso"];
            int IdProfissional = 2;

            if (nivelAcesso == 2)
            {
                IdProfissional = (int)Session["idProfissional"];
            }

            ViewBag.ProfissionalId = listaProfissional(IdProfissional);

            
            return View();
        }

        // POST: Consulta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LiberacaoViewModel liberacao)
        {

            if (liberacao.PacienteId == 1)
            {
                ModelState.AddModelError(string.Empty, @"Paciente Selecionado Invalido");

                return View(liberacao);
            }

            if (ModelState.IsValid)
            {
                var liberacaoDomain = Mapper.Map<LiberacaoViewModel, Liberacao>(liberacao);

                if (!String.IsNullOrEmpty(liberacaoDomain.MedicoEncaminhante))
                { 
                    liberacaoDomain.MedicoEncaminhante = liberacaoDomain.MedicoEncaminhante.ToUpper();
                }

                liberacaoDomain.QuantidadeRealizadaExterno = liberacaoDomain.QuantidadeRealizada;

                _liberacaoApp.Add(liberacaoDomain);

                for (int i = 0; i < liberacaoDomain.QuantidadeTotal-liberacaoDomain.QuantidadeRealizada; i++)
                {
                    var consultaDomain = new Consulta();
                    consultaDomain.DataCadastro = DateTime.Now;
                    consultaDomain.LiberacaoId = liberacaoDomain.LiberacaoId;
                    consultaDomain.Convenio = "Unimed";
                    consultaDomain.Status = "Pré-agendado";
                    consultaDomain.ValorConsulta = 0;
                    consultaDomain.ValorConvenio = 0;
                    consultaDomain.ValorCopart = 0;
                    
                    consultaDomain.ProfissionalId = liberacaoDomain.ProfissionalId;
                    
                    _consultaApp.Add(consultaDomain);
                }

                return RedirectToAction("Details", "Liberacoes", new { id = liberacaoDomain.LiberacaoId });

                //liberacaoDomain.Paciente = _pacienteApp.GetById(liberacaoDomain.PacienteId);

                //var liberacaoViewModel = Mapper.Map<Liberacao, LiberacaoViewModel>(liberacaoDomain);

                //var consultasViewModel = Mapper.Map<IEnumerable<Consulta>, IEnumerable<ConsultaViewModel>>(_consultaApp.GetAll());

                //consultasViewModel = consultasViewModel.Where(s => s.LiberacaoId == liberacaoDomain.LiberacaoId);

                //var tuple = new Tuple<LiberacaoViewModel, IEnumerable<ConsultaViewModel>>(liberacaoViewModel, consultasViewModel);

                //return View("Details",tuple);
            }

            ViewBag.PacienteId = listaPaciente(1);

            var nivelAcesso = (int)Session["nivelAcesso"];
            int IdProfissional= 2;

            if (nivelAcesso == 2)
            {
                IdProfissional = (int)Session["idProfissional"];
            }

            ViewBag.ProfissionalId = listaProfissional(IdProfissional);

            return View(liberacao);
        }

        // GET: Consulta/Edit/5
        public ActionResult Edit(int id)
        {
            var liberacao = _liberacaoApp.GetById(id);
            var liberacaoViewModel = Mapper.Map<Liberacao, LiberacaoViewModel>(liberacao);

            ViewBag.PacienteId = listaPaciente(liberacaoViewModel.PacienteId);
            ViewBag.ProfissionalId = listaProfissional(liberacaoViewModel.ProfissionalId);

            return View(liberacaoViewModel);
        }

        // POST: Consulta/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LiberacaoViewModel liberacao)
        {
            if (liberacao.PacienteId == 3)
            {
                ModelState.AddModelError(string.Empty, @"Paciente Selecionado Invalido");

                return View(liberacao);
            }

            if (ModelState.IsValid)
            {
                var liberacaoDomain = Mapper.Map<LiberacaoViewModel, Liberacao>(liberacao);
                _liberacaoApp.Update(liberacaoDomain);

                return RedirectToAction("Index");
            }


            ViewBag.PacienteId = listaPaciente(liberacao.PacienteId);
            ViewBag.ProfissionalId = listaProfissional(liberacao.ProfissionalId);

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

        public IEnumerable<SelectListItem> listaProfissional(int idProfissional)
        {
            IEnumerable<SelectListItem> selectListProfissional =
               from c in _profissionalApp.GetAll().OrderBy(p => p.NomeProfissional)
               select new SelectListItem
               {
                   Selected = (c.ProfissionalId == idProfissional),
                   Text = c.NomeProfissional,
                   Value = c.ProfissionalId.ToString()
               };

            return selectListProfissional;
        }

        public IEnumerable<SelectListItem> listaPaciente(int idPaciente)
        {
            IEnumerable<SelectListItem> selectListPaciente =
               from c in _pacienteApp.GetAll().OrderBy(p => p.NomePaciente)
               select new SelectListItem
               {
                   Selected = (c.PacienteId == idPaciente),
                   Text = c.NomePaciente,
                   Value = c.PacienteId.ToString()
               };

            return selectListPaciente;
        }


        public IEnumerable<Liberacao> Paginar(IEnumerable<Liberacao> listConsulta, String paginaAtual, int listaPorPagina)
        {
            if (String.IsNullOrEmpty(paginaAtual))
            {
                paginaAtual = "1";
            }

            ViewBag.TotalPage = listConsulta.Count() / listaPorPagina;

            listConsulta = listConsulta.Skip((int.Parse(paginaAtual) - 1) * listaPorPagina);
            listConsulta = listConsulta.Take(listaPorPagina);

            ViewBag.CurrentPage = int.Parse(paginaAtual);

            if (ViewBag.CurrentPage == 1)
            {
                ViewBag.startPage = 1;
            }
            else
            {
                ViewBag.startPage = @ViewBag.CurrentPage - 1;
            }

            if ((ViewBag.CurrentPage + 5) < ViewBag.TotalPage)
            {
                ViewBag.endPage = ViewBag.CurrentPage + 5;
            }
            else
            {
                ViewBag.endPage = ViewBag.TotalPage;
            }

            return listConsulta;
        }

    }

}