using AutoMapper;
using ProjetoModeloDDD.Application.Interface;
using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.MVC.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ProjetoModeloDDD.MVC.Controllers
{
    public class ProducaoController : Controller
    {

        private readonly IProducaoAppService _producaoApp;

        public ProducaoController(IProducaoAppService producaoApp)
        {
            _producaoApp = producaoApp;

        }
   


        // GET: Producao
        //public ActionResult Index(string palavra, int? LocalizarPor)
        //{

        //    var producaoViewModel = Mapper.Map<IEnumerable<Producao>, IEnumerable<ProducaoViewModel>>(_producaoApp.GetAll());
        //    int idLocalizacao = LocalizarPor.GetValueOrDefault();

        //    if(!String.IsNullOrEmpty(palavra))
        //    {
        //        switch(idLocalizacao)
        //        {
        //            case 2:
        //                producaoViewModel = producaoViewModel.Where(p => p.NomePaciente.Contains(palavra));
        //                break;
        //            case 1:
        //                producaoViewModel = producaoViewModel.Where(p => p.CarteirinhaPaciente.Contains(palavra));
        //                break;
        //        }
        //    }

        //    return View(producaoViewModel);

        //}


        // GET: Producao
        public ActionResult Index()
        {

            var producaoViewModel = Mapper.Map<IEnumerable<Producao>, IEnumerable<ProducaoViewModel>>(_producaoApp.GetAll());
           

            return View(producaoViewModel);

        }




    }
}

