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
        public ActionResult Index(string palavra, int? LocalizarPor)
        {
            IEnumerable<LiberacaoViewModel> liberacaoViewModel;

            if (Session["Usuario"] == null)
            {
                return RedirectToAction("index", "login");
            }

            int idLocalizacao = LocalizarPor.GetValueOrDefault();

            if (!String.IsNullOrEmpty(palavra))
            {
                liberacaoViewModel = Mapper.Map<IEnumerable<Liberacao>, IEnumerable<LiberacaoViewModel>>(_liberacaoApp.GetAll());

                switch (idLocalizacao)
                {
                    case 1:
                        liberacaoViewModel = liberacaoViewModel.Where(s => s.NumeroLiberacao.Contains(palavra));
                        break;
                    case 2:
                        liberacaoViewModel = liberacaoViewModel.Where(s => s.Paciente.NomePaciente.Contains(palavra));
                        break;
                }

                var nivelAcesso = (int)Session["nivelAcesso"];
                if (nivelAcesso == 2)
                {
                    var IdProfissional = (int)Session["idProfissional"];

                    liberacaoViewModel = liberacaoViewModel.Where(s => s.ProfissionalId == IdProfissional);
                }

            }
            else
            {
                liberacaoViewModel = new List<LiberacaoViewModel> { new LiberacaoViewModel() };
                liberacaoViewModel.ToList().First().Paciente = new PacienteViewModel();
                liberacaoViewModel.ToList().First().Profissional = new ProfissionalViewModel();
            }


            return View(liberacaoViewModel);
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
            ViewBag.PacienteId = new SelectList(_pacienteApp.GetAll(), "PacienteId", "NomePaciente");
            ViewBag.ProfissionalId = new SelectList(_profissionalApp.GetAll(), "ProfissionalId", "NomeProfissional");

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

                for (int i = 0; i < liberacaoDomain.QuantidadeTotal; i++)
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

            ViewBag.PacienteId = new SelectList(_pacienteApp.GetAll(), "PacienteId", "NomePaciente");
            ViewBag.ProfissionalId = new SelectList(_profissionalApp.GetAll(), "ProfissionalId", "NomeProfissional");

            return View(liberacao);
        }

        // GET: Consulta/Edit/5
        public ActionResult Edit(int id)
        {
            var liberacao = _liberacaoApp.GetById(id);
            var liberacaoViewModel = Mapper.Map<Liberacao, LiberacaoViewModel>(liberacao);

            ViewBag.PacienteId = listaPaciente(liberacaoViewModel);
            ViewBag.ProfissionalId = listaProfissional(liberacaoViewModel);

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


            ViewBag.PacienteId = listaPaciente(liberacao);
            ViewBag.ProfissionalId = listaProfissional(liberacao);

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

        public IEnumerable<SelectListItem> listaProfissional(LiberacaoViewModel liberacao)
        {
            IEnumerable<SelectListItem> selectListProfissional =
               from c in _profissionalApp.GetAll()
               select new SelectListItem
               {
                   Selected = (c.ProfissionalId == liberacao.Profissional.ProfissionalId),
                   Text = c.NomeProfissional,
                   Value = c.ProfissionalId.ToString()
               };

            return selectListProfissional;
        }

        public IEnumerable<SelectListItem> listaPaciente(LiberacaoViewModel liberacao)
        {
            IEnumerable<SelectListItem> selectListPaciente =
               from c in _pacienteApp.GetAll()
               select new SelectListItem
               {
                   Selected = (c.PacienteId == liberacao.PacienteId),
                   Text = c.NomePaciente,
                   Value = c.PacienteId.ToString()
               };

            return selectListPaciente;
        }

    }

}