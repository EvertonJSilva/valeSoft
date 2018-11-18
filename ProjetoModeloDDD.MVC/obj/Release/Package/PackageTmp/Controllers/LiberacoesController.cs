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
        public ActionResult Index(string palavra, int? LocalizarPor, string grid1page, bool? criterio)
        {
            IEnumerable<Liberacao> listaLiberacao = Enumerable.Empty<Liberacao>();

            if (Session["Usuario"] == null)
            {
                TempData["info"] = "Você não está logado no sistema.";
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

            switch (criterio)
            {
                case true:
                    break;
                default:
                    listaLiberacao = listaLiberacao.Where(s => s.QuantidadeTotal != s.QuantidadeRealizada);
                    break;
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

            var consultasViewModel = Mapper.Map<IEnumerable<Consulta>, IEnumerable<ConsultaViewModel>>(_consultaApp.GetPorIdProfissional(0, "", liberacao.NumeroLiberacao));

            //consultasViewModel = consultasViewModel.Where(s => s.LiberacaoId == id);

            var tuple = new Tuple<LiberacaoViewModel, IEnumerable<ConsultaViewModel>>(liberacaoViewModel, consultasViewModel);

            return View(tuple);
        }

        // GET: Consulta/Create
        public ActionResult Create()
        {
                popularviewBag(); 
                return View();
        }

        private void popularviewBag()
        {
            ViewBag.PacienteId = listaPaciente(1136);

            var nivelAcesso = (int)Session["nivelAcesso"];
            int IdProfissional = 2;

            if (nivelAcesso == 2)
            {
                IdProfissional = (int)Session["idProfissional"];
            }
            ViewBag.ProfissionalId = listaProfissional(IdProfissional);
        }

        // POST: Consulta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(LiberacaoViewModel liberacao)
        {
                //if (liberacao.PacienteId == 1)
                //{
                //  ModelState.AddModelError(string.Empty, @"Paciente Selecionado Invalido");

                //return View(liberacao);
                //}

                if (ModelState.IsValid)
                {
                try
                {

                    var liberacaoDomain = Mapper.Map<LiberacaoViewModel, Liberacao>(liberacao);

                    if (!String.IsNullOrEmpty(liberacaoDomain.MedicoEncaminhante))
                    {
                        liberacaoDomain.MedicoEncaminhante = liberacaoDomain.MedicoEncaminhante.ToUpper();
                    }

                    liberacaoDomain.QuantidadeRealizadaExterno = liberacaoDomain.QuantidadeRealizada;
                    if (liberacao.PacienteId == 1136) // nao deixa salvar paciente selecione
                    {
                        //ModelState.AddModelError(string.Empty, @"Paciente Selecionado Invalido");
                        TempData["error"] = "O paciente Selecionado é inválido";
                        popularviewBag();
                        return View(liberacao);
                    }

                    if (liberacao.ProfissionalId == 2) // nao deixa salvar profissional selecione
                    {
                        //ModelState.AddModelError(string.Empty, @"Profissional Selecionado Invalido");
                        TempData["error"] = "O profissional Selecionado é inválido";
                        popularviewBag();
                        return View(liberacao);
                    }

                    _liberacaoApp.Add(liberacaoDomain);

                    for (int i = 0; i < liberacaoDomain.QuantidadeTotal - liberacaoDomain.QuantidadeRealizada; i++)
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

                    TempData["success"] = "Liberação incluída com sucesso.";
                    return RedirectToAction("Details", "Liberacoes", new { id = liberacaoDomain.LiberacaoId });

                }
                catch (Exception)
                {
                    TempData["error"] = "Não foi possível incluir a liberação.";
                    return View(liberacao);
                }

                //liberacaoDomain.Paciente = _pacienteApp.GetById(liberacaoDomain.PacienteId);

                //var liberacaoViewModel = Mapper.Map<Liberacao, LiberacaoViewModel>(liberacaoDomain);

                //var consultasViewModel = Mapper.Map<IEnumerable<Consulta>, IEnumerable<ConsultaViewModel>>(_consultaApp.GetAll());

                //consultasViewModel = consultasViewModel.Where(s => s.LiberacaoId == liberacaoDomain.LiberacaoId);

                //var tuple = new Tuple<LiberacaoViewModel, IEnumerable<ConsultaViewModel>>(liberacaoViewModel, consultasViewModel);

                //return View("Details",tuple);
            }                
            ViewBag.PacienteId = listaPaciente(1);

            try
            {
                var nivelAcesso = (int)Session["nivelAcesso"];
                int IdProfissional = 2;

                if (nivelAcesso == 2)
                {
                    IdProfissional = (int)Session["idProfissional"];
                }

                ViewBag.ProfissionalId = listaProfissional(IdProfissional);
                return View(liberacao);
            }
            catch (Exception)
            {
                TempData["error"] = "Não foi possível incluir a liberação.";
                return View(liberacao);
            }            
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
            try
            {
                //  if (liberacao.PacienteId == 3)
                //{
                //  ModelState.AddModelError(string.Empty, @"Paciente Selecionado Invalido");

                //return View(liberacao);
                //}

                if (ModelState.IsValid)
                {
                    var liberacaoDomain = Mapper.Map<LiberacaoViewModel, Liberacao>(liberacao);
                    _liberacaoApp.Update(liberacaoDomain);

                    TempData["success"] = "Liberação editada com sucesso.";
                    return RedirectToAction("Index");
                }


                ViewBag.PacienteId = listaPaciente(liberacao.PacienteId);
                ViewBag.ProfissionalId = listaProfissional(liberacao.ProfissionalId);

                return View(liberacao);
            }
            catch (Exception)
            {
                TempData["error"] = "Esta liberação não pode ser editada.";
                return View(liberacao);
            }

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
            try
            {
                var liberacao = _liberacaoApp.GetById(id);
                _liberacaoApp.Remove(liberacao);

                TempData["success"] = "Liberação excluída com sucesso.";
                return RedirectToAction("Index");
        }
            catch (Exception)
            {
                TempData["error"] = "Esta liberação não pode ser excluída pois uma ou mais consultas já estão agendadas.";
                return RedirectToAction("Index");
    }
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

            ViewBag.TotalPage = (int)Math.Ceiling((double)listConsulta.Count() / listaPorPagina);

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