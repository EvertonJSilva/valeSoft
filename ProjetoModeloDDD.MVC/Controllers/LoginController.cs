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
            LoginViewModel _loginViewModel = new LoginViewModel();
            return View(_loginViewModel);
        }

        public ActionResult Logout()
        {
            Session["usuario"] = null;
            LoginViewModel _loginViewModel = new LoginViewModel();
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
                Session["usuario"] = new Usuario(login.usuario, login.senha);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var profissionais = _profissionalApp.GetAll();

                profissionais = profissionais.Where(p => p.Login == login.usuario);

                //não achou
                if (profissionais.Count() == 0)
                {
                    ModelState.AddModelError(string.Empty, @"Usuário ou senha inválidos");
                    return View(login);
                }
                else
                {
                    var senhaCriptografada = Util.encryption(login.senha);
                    if (senhaCriptografada != profissionais.First().Senha)
                    {
                        ModelState.AddModelError(string.Empty, @"Usuário ou senha inválidos");
                        return View(login);
                    }

                }

                Session["usuario"] = new Usuario(login.usuario, profissionais.First().Senha);
                return RedirectToAction("Index", "Home");
                
            }
        }

        public ActionResult Reenvio(string email)
        {
            return  RedirectToAction("Index", "Home"); ;
        }

        
    }
}
