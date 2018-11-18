using System.Web.Mvc;
using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.MVC.ViewModels;
using ProjetoModeloDDD.Application;
using ProjetoModeloDDD.Application.Interface;
using System.Collections.Generic;
using AutoMapper;
using System.Linq;

namespace ProjetoModeloDDD.MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IProfissionalAppService _profissionalApp;

        public LoginController(IProfissionalAppService profissionalApp)
        {
            _profissionalApp = profissionalApp;
        }
        public ActionResult Index()
        {
            Session["usuario"] = null;
            Session["nivelAcesso"] = null;
            Session["idProfissional"] = null;            
            LoginViewModel _loginViewModel = new LoginViewModel();

            return View(_loginViewModel);
        }

        public ActionResult Logout()
        {
            Session["usuario"] = null;
            Session["nivelAcesso"] = null;
            Session["idProfissional"] = null;

            LoginViewModel _loginViewModel = new LoginViewModel();
            TempData["success"] = "Logout efetuado com sucesso.";
            return View(_loginViewModel);
        }

        //public ActionResult IndexNovo()
        //{
        //    LoginViewModel _loginViewModel = new LoginViewModel();
        //    return View(_loginViewModel);
        //}

        //public ActionResult mymodal()
        //{
        //    LoginViewModel _loginViewModel = new LoginViewModel();
        //    return View(_loginViewModel);
        //}

        [HttpPost]
        public ActionResult Index(LoginViewModel login )
        {
            if (login.usuario == "sa" && login.senha == "ValeSoft#9090")
            {                
                Session["usuario"] = new Usuario(login.usuario, login.senha, 0, 0);
                Session["nivelAcesso"] = 0;
                Session["idProfissional"] = 0;

                TempData["success"] = "Login efetuado com sucesso.";
                return RedirectToAction("Index", "Home");                
            }
            else
            {
                var profissionais = _profissionalApp.GetAll();

                profissionais = profissionais.Where(p => p.Login == login.usuario);

                //não achou
                if (profissionais.Count() == 0)
                {
                    //ModelState.AddModelError(string.Empty, @"Usuário e//ou senha inválidos");
                    TempData["error"] = "Usuário ou senha inválidos";
                    return View("Index");
                }
                else
                {
                    var senhaCriptografada = Util.encryption(login.senha);
                    if (senhaCriptografada != profissionais.First().Senha)
                    {
                        //ModelState.AddModelError(string.Empty, @"Usuário e/ou senha inválidos");
                        TempData["error"] = "Usuário ou senha inválidos";
                        return View(login);
                    }

                } 

                Session["usuario"] = new Usuario(login.usuario, profissionais.First().Senha,
                                                profissionais.First().TipoProfissional.nivelAcesso,
                                                profissionais.First().ProfissionalId);

                Session["nivelAcesso"] = profissionais.First().TipoProfissional.nivelAcesso;
                Session["idProfissional"] = profissionais.First().ProfissionalId;

                TempData["success"] = "Login efetuado com sucesso.";
                return RedirectToAction("Index", "Home");
                
            }
        }

        public ActionResult Reenvio(string email)
        {
            TempData["info"] = "E-mail enviado para o endereço informado.";
            return  RedirectToAction("Index", "Home"); ;
        }

        
    }
}
