using System.Web.Mvc;
using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.MVC.ViewModels;

namespace ProjetoModeloDDD.MVC.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            LoginViewModel _loginViewModel = new LoginViewModel();
            return View(_loginViewModel);
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel login )
        {
            Session["usuario"] = new Usuario(login.usuario, login.senha);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Reenvio()
        {
            return View();
        }

    }
}
