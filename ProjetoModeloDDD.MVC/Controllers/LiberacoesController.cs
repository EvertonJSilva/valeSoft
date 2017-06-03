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


        public LiberacoesController(ILiberacaoAppService liberacaoApp, IPacienteAppService pacienteApp, IConsultaAppService consultaApp)
        {
            _liberacaoApp = liberacaoApp;
            _pacienteApp = pacienteApp;
            _consultaApp = consultaApp;
        }

        // GET: Consulta
        public ActionResult Index(string palavra, int? LocalizarPor)
        {
            var liberacaoViewModel = Mapper.Map<IEnumerable<Liberacao>, IEnumerable<LiberacaoViewModel>>(_liberacaoApp.GetAll());
            int idLocalizacao = LocalizarPor.GetValueOrDefault();

            if (!String.IsNullOrEmpty(palavra))
            {
                switch (idLocalizacao)
                {
                    case 1:
                        liberacaoViewModel = liberacaoViewModel.Where(s => s.NumeroLiberacao.Contains(palavra));
                        break;
                    case 2:
                        liberacaoViewModel = liberacaoViewModel.Where(s => s.Paciente.NomePaciente.Contains(palavra));
                        break;
                }

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

                for (int i = 0; i <= liberacaoDomain.QuantidadeTotal; i++)
                {
                    var consultaDomain = new Consulta();
                    consultaDomain.LiberacaoId = liberacaoDomain.LiberacaoId;
                    consultaDomain.Convenio = "Unimed";
                    consultaDomain.Status = "Pré-agendado";
                    
                    //não informado/
                    consultaDomain.ProfissionalId = 1;
                    
                    _consultaApp.Add(consultaDomain);
                }

                return RedirectToAction("Details","Liberacoes", new { id = liberacaoDomain.LiberacaoId });

                //liberacaoDomain.Paciente = _pacienteApp.GetById(liberacaoDomain.PacienteId);

                //var liberacaoViewModel = Mapper.Map<Liberacao, LiberacaoViewModel>(liberacaoDomain);

                //var consultasViewModel = Mapper.Map<IEnumerable<Consulta>, IEnumerable<ConsultaViewModel>>(_consultaApp.GetAll());

                //consultasViewModel = consultasViewModel.Where(s => s.LiberacaoId == liberacaoDomain.LiberacaoId);

                //var tuple = new Tuple<LiberacaoViewModel, IEnumerable<ConsultaViewModel>>(liberacaoViewModel, consultasViewModel);

                //return View("Details",tuple);
            }
            
            ViewBag.PacienteId = new SelectList(_pacienteApp.GetAll(), "PacienteId", "NomePaciente");

            return View(liberacao);
        }

        // GET: Consulta/Edit/5
        public ActionResult Edit(int id)
        {
            var liberacao = _liberacaoApp.GetById(id);
            var liberacaoViewModel = Mapper.Map<Liberacao, LiberacaoViewModel>(liberacao);

            ViewBag.PacienteId = listaPaciente(liberacaoViewModel);
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
