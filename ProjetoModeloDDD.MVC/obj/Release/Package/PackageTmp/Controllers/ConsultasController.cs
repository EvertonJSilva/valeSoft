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
        public ActionResult Index(string palavra, int? LocalizarPor, string grid1page, bool? somenteMes)
        {
            if (Session["Usuario"] == null)
            {
                TempData["info"] = "Você não está logado no sistema.";
                return RedirectToAction("index", "login");
            }
            if (String.IsNullOrEmpty(grid1page))
            {
                grid1page = "1";
            }


            IEnumerable<Consulta> listConsulta = Enumerable.Empty<Consulta>();

            // médicos só vêem as consultas dele
            var nivelAcesso = (int)Session["nivelAcesso"];
            int IdProfissional = 0;
            if (nivelAcesso == 2)
            {
                IdProfissional = (int)Session["idProfissional"];

            }

            int idLocalizacao = LocalizarPor.GetValueOrDefault();

            if (!String.IsNullOrEmpty(palavra))
            {
                switch (idLocalizacao)
                {
                    case 2:
                        listConsulta = _consultaApp.GetPorIdProfissional(IdProfissional, palavra.ToLower(), "");
                        break;
                    case 1:
                        listConsulta = _consultaApp.GetPorIdProfissional(IdProfissional,"" ,palavra.ToLower());
                        break;
                }

            }
            else
            {
                listConsulta = _consultaApp.GetPorIdProfissional(IdProfissional, "", "");
            }

            switch (somenteMes)
            {
                case false:
                    break;
                default:
                    if(nivelAcesso == 2)
                        listConsulta = listConsulta.Where(s => s.DataHoraConsulta.Month == DateTime.Now.Month );
                    
                    break;
            }

            listConsulta = listConsulta.OrderByDescending(x => x.Status);
            listConsulta = Paginar(listConsulta, grid1page, 20);



            var consultaViewModel = Mapper.Map<IEnumerable<Consulta>, IEnumerable<ConsultaViewModel>>(listConsulta);

            

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

            var valoresConsultas = ListaValoresConsultas(liberacao.Paciente, sessao);

            var valor = valoresConsultas.FirstOrDefault(c => c.Selected);
            
            return Json(valor, JsonRequestBehavior.AllowGet);
        }

        public IEnumerable<ListaValores> ListaValoresConsultas(Paciente paciente, string sessao)
        {
            IEnumerable<ListaValores> selectListvalores =
                          from c in _valorApp.GetPorSigla(paciente.CarteirinhaPaciente.Substring(0, 4))
                          select new ListaValores(c, paciente)
                          {
                              Selected = (c.Sessao == sessao)
                          };

            return selectListvalores;

        }

        // GET: Consulta/Create
        public ActionResult Create()
        {
            var nivelAcesso = (int)Session["nivelAcesso"];

            if (nivelAcesso == 2)
            {
                var IdProfissional = (int)Session["idProfissional"];
                ViewBag.ProfissionalId = listaProfissional(IdProfissional);
            }
            else
            {
                ViewBag.ProfissionalId = new SelectList(_profissionalApp.GetAll(), "ProfissionalId", "NomeProfissional");
            }

            ViewBag.LiberacaoId = new SelectList(_liberacaoApp.GetAll(), "LiberacaoId", "NumeroLiberacao");

            return View();
        }


        // POST: Consulta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ConsultaViewModel consulta)
        {
            try
            {
                if (consulta.TipoSessao != "80000509" && String.IsNullOrEmpty(consulta.Autorizacao))
                {
                    //ModelState.AddModelError(string.Empty, @"Autorização deve ser preenchida");
                    TempData["warning"] = "Preencha a autorização.";
                    return View(consulta);
                }

                // if(consulta.Liberacao.PacienteId == 1)
                //{
                ///  ModelState.AddModelError(string.Empty, @"Paciente Selecionado Invalido");

                //return View(consulta);
                //}

                if (consulta.ProfissionalId == 2)
                {
                    //ModelState.AddModelError(string.Empty, @"Profissional não selecionado");
                    TempData["warning"] = "Profissional inválido.";
                    return View(consulta);
                }

                if (consulta.DataHoraConsulta.Year == 1)
                {
                    //ModelState.AddModelError(string.Empty, @"Data selecionada inválida");
                    TempData["warning"] = "Data inválida.";
                    return View(consulta);
                }

                if (new DateTime(consulta.DataHoraConsulta.Year,consulta.DataHoraConsulta.Month,1) != (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)))
                //if (consulta.DataHoraConsulta != (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)))
                {
                    //ModelState.AddModelError(string.Empty, @"Data da consulta inferior a início do mês");
                    TempData["warning"] = "Mês da consulta diferente do mês atual.";
                    return View(consulta);
                }


                if (ModelState.IsValid)
                {

                    var consultaDomain = Mapper.Map<ConsultaViewModel, Consulta>(consulta);
                    consultaDomain.DataCadastro = DateTime.Now;
                    _consultaApp.Add(consultaDomain);

                    TempData["success"] = "Consulta incluída com sucesso.";
                    return RedirectToAction("Index");
                }

                ViewBag.LiberacaoId = listaLiberacao(consulta);
                ViewBag.ProfissionalId = listaProfissional(consulta);



                return View(consulta);
            }
            catch (Exception)
            {
                TempData["error"] = "Não foi possível incluir a consulta.";
                return View();
            }
    }

        // GET: Consulta/Edit/5
        public ActionResult Edit(int id, int idLiberacao)
        {
                var consulta = _consultaApp.GetById(id);
                var consultaViewModel = Mapper.Map<Consulta, ConsultaViewModel>(consulta);

                ViewBag.idLiberacaoOrigem = idLiberacao;

                // médicos já sugere a matricula dele
                var nivelAcesso = (int)Session["nivelAcesso"];
                if (consultaViewModel.Status == "Pré-agendado")
                {
                    consultaViewModel.DataHoraConsulta = DateTime.Now;

                    if (nivelAcesso == 2)
                    {
                        var IdProfissional = (int)Session["idProfissional"];
                        consultaViewModel.ProfissionalId = IdProfissional;
                    }
                }


                ViewBag.LiberacaoId = listaLiberacao(consultaViewModel);
                ViewBag.ProfissionalId = listaProfissional(consultaViewModel);

                return View(consultaViewModel);
        }

        // POST: Consulta/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ConsultaViewModel consulta, int idLiberacaoOrigem)
        {
            try { 
            ViewBag.idLiberacaoOrigem = idLiberacaoOrigem;
            ViewBag.LiberacaoId = listaLiberacao(consulta);
            ViewBag.ProfissionalId = listaProfissional(consulta);

            if (consulta.TipoSessao != "80000509" && String.IsNullOrEmpty(consulta.Autorizacao))
            {
               //ModelState.AddModelError(string.Empty, @"Autorização deve ser preenchida");
                    TempData["warning"] = "Preencha a autorização.";
                    return View(consulta);
            }

            //if (consulta.Liberacao.PacienteId == 1)
            //{
              //  ModelState.AddModelError(string.Empty, @"Paciente Selecionado Invalido");

                //return View(consulta);
            //}

            if (consulta.ProfissionalId == 2)
            {
                //ModelState.AddModelError(string.Empty, @"Profissional não selecionado");
                    TempData["warning"] = "Profissional inválido.";
                    return View(consulta);
            }

            if (consulta.DataHoraConsulta.Year == 1)
            {
                //ModelState.AddModelError(string.Empty, @"Data selecionada inválida");
                    TempData["warning"] = "Data inválida.";
                    return View(consulta);
            }

            //médicos valida data
            var nivelAcesso = (int)Session["nivelAcesso"];
            if (nivelAcesso == 2)
            {
                if (new DateTime(consulta.DataHoraConsulta.Year, consulta.DataHoraConsulta.Month, 1) != (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)))
                //if ( DateTime.Now != (new DateTime(consulta.DataHoraConsulta.Year, consulta.DataHoraConsulta.Month, 10).AddMonths(1)))
                {
                //ModelState.AddModelError(string.Empty, @"Data da consulta inferior a início do mês");
                    TempData["warning"] = "Mês da consulta diferente do mês atual.";
                    return View(consulta);
                }
            }

            if (ModelState.IsValid)
            {
                var consultaDomain = Mapper.Map<ConsultaViewModel, Consulta>(consulta);
                consultaDomain.Status = "Agendado";

                _consultaApp.Update(consultaDomain);

                _liberacaoApp.AtualizarConsultasRealizadas(_liberacaoApp.GetById(consultaDomain.LiberacaoId));

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

                //redireciona para a liberacao
                //if (idLiberacaoOrigem > 0) {
                //  return RedirectToAction("Details", "Liberacoes", new { id = idLiberacaoOrigem });
                //} else
                //{
                //  return RedirectToAction("Edit");
                //}
            }

                TempData["success"] = "Consulta editada com sucesso.";
                return View(consulta);
            }
            catch (Exception)
            {
                TempData["error"] = "Não foi possível editar a consulta.";
                return View(consulta);
            }
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
            try
            {
                var consulta = _consultaApp.GetById(id);
                _consultaApp.Remove(consulta);

                TempData["success"] = "Consulta excluída com sucesso.";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["error"] = "Não foi possível excluir a consulta.";
                return View();
            }
        }

        public IEnumerable<SelectListItem> listaProfissional(int idProfissional)
        {
            IEnumerable<SelectListItem> selectListProfissional =
               from c in _profissionalApp.GetAll()
               select new SelectListItem
               {
                   Selected = (c.ProfissionalId == idProfissional),
                   Text = c.NomeProfissional,
                   Value = c.ProfissionalId.ToString()
               };

            return selectListProfissional;
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

        public IEnumerable<Consulta> Paginar(IEnumerable<Consulta> listConsulta, String paginaAtual, int listaPorPagina)
        {

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


    public class ListaValores : SelectListItem
    {

        public decimal _ValorConsulta { get; set; }
        public decimal _ValorCopart { get; set; }
        public decimal _ValorConvenio { get; set; }

        public ListaValores(ValorConsulta valorConsulta, Paciente paciente)
        {
            this.Value = valorConsulta.Sessao;
            this.Text = CalculaTexto(valorConsulta.Sessao);
            this._ValorConsulta = valorConsulta.Valor;
            this._ValorCopart = CalculaCopart(valorConsulta, paciente);
            this._ValorConvenio = CalculaConvenio(valorConsulta, paciente);
        }

        private decimal CalculaCopart(ValorConsulta valorConsulta, Paciente paciente)
        {

            if (paciente.PacienteId == 779) // Larisa valor fixo de 10 
                return 10;

            if (paciente.PacienteId == 748) // Juliana
                return 10;

            switch (valorConsulta.TemCopart && paciente.CopartPaciente)
            {
                
                case true:
                    return 15;
                case false:
                    return 0;
                default:
                    return 0;
            }
        }

        private decimal CalculaConvenio(ValorConsulta valorConsulta, Paciente paciente)
        {

            if (paciente.PacienteId == 779) // larrisa valor fixo de 10 
                return valorConsulta.Valor - 10;

            //if (paciente.PacienteId == 748) // juliana 
                //return valorConsulta.Valor - 10;

            switch (valorConsulta.TemCopart && paciente.CopartPaciente)
            {
                case true:
                    return valorConsulta.Valor - 15;
                case false:
                    return valorConsulta.Valor;
                default:
                    return 0;
            }
        }

        private string CalculaTexto(string sessao)
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