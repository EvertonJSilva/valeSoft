using ProjetoModeloDDD.Application;
using ProjetoModeloDDD.Application.Interface;
using ProjetoModeloDDD.Domain.Interfaces.Repositories;
using ProjetoModeloDDD.Domain.Interfaces.Services;
using ProjetoModeloDDD.Domain.Services;
using ProjetoModeloDDD.Infra.Data.Repositories;


[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ProjetoModeloDDD.MVC.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(ProjetoModeloDDD.MVC.App_Start.NinjectWebCommon), "Stop")]

namespace ProjetoModeloDDD.MVC.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(typeof(IAppServiceBase<>)).To(typeof(AppServiceBase<>));
            kernel.Bind<IClienteAppService>().To<ClienteAppService>();
            kernel.Bind<IProdutoAppService>().To<ProdutoAppService>();
            kernel.Bind<IConsultaAppService>().To<ConsultaAppService>();
            kernel.Bind<IFolhaAppService>().To<FolhaAppService>();
            kernel.Bind<IFolhaDetalheAppService>().To<FolhaDetalheAppService>();
            kernel.Bind<ILiberacaoAppService>().To<LiberacaoAppService>();
            kernel.Bind<IPacienteAppService>().To<PacienteAppService>();
            kernel.Bind<IProfissionalAppService>().To<ProfissionalAppService>();
            kernel.Bind<ITaxaDoacaoAppService>().To<TaxaDoacaoAppService>();
            kernel.Bind<IValorConsultaAppService>().To<ValorConsultaAppService>();
            kernel.Bind<IProducaoAppService>().To<ProducaoAppService>();
            kernel.Bind<ITipoProfissionalAppService>().To<TipoProfissionalAppService>();
            kernel.Bind<ITaxaExtraProfissionalAppService>().To<TaxaExtraProfissionalAppService>();
            kernel.Bind<IDemonstrativoReportAppService>().To<DemonstrativoReportAppService>();


            kernel.Bind(typeof(IServiceBase<>)).To(typeof(ServiceBase<>));
            kernel.Bind<IClienteService>().To<ClienteService>();
            kernel.Bind<IProdutoService>().To<ProdutoService>();
            kernel.Bind<IConsultaService>().To<ConsultaService>();
            kernel.Bind<IFolhaService>().To<FolhaService>();
            kernel.Bind<IFolhaDetalheService>().To<FolhaDetalheService>();
            kernel.Bind<ILiberacaoService>().To<LiberacaoService>();
            kernel.Bind<IPacienteService>().To<PacienteService>();
            kernel.Bind<IProfissionalService>().To<ProfissionalService>();
            kernel.Bind<ITaxaDoacaoService>().To<TaxaDoacaoService>();
            kernel.Bind<IValorConsultaService>().To<ValorConsultaService>();
            kernel.Bind<IProducaoService>().To<ProducaoService>();
            kernel.Bind<ITipoProfissionalService>().To<TipoProfissionalService>();
            kernel.Bind<ITaxaExtraProfissionalService>().To<TaxaExtraProfissionalService>();
            

            kernel.Bind(typeof(IRepositoryBase<>)).To(typeof(RepositoryBase<>));
            kernel.Bind<IClienteRepository>().To<ClienteRepository>();
            kernel.Bind<IProdutoRepository>().To<ProdutoRepository>();
            kernel.Bind<IConsultaRepository>().To<ConsultaRepository>();
            kernel.Bind<IFolhaRepository>().To<FolhaRepository>();
            kernel.Bind<IFolhaDetalheRepository>().To<FolhaDetalheRepository>();
            kernel.Bind<ILiberacaoRepository>().To<LiberacaoRepository>();
            kernel.Bind<IPacienteRepository>().To<PacienteRepository>();
            kernel.Bind<IProfissionalRepository>().To<ProfissionalRepository>();
            kernel.Bind<ITaxaDoacaoRepository>().To<TaxaDoacaoRepository>();
            kernel.Bind<IValorConsultaRepository>().To<ValorConsultaRepository>();
            kernel.Bind<IProducaoRepository>().To<ProducaoRepository>();
            kernel.Bind<ITipoProfissionalRepository>().To<TipoProfissionalRepository>();
            kernel.Bind<ITaxaExtraProfissionalRepository>().To<TaxaExtraProfissionalRepository>();


        }
    }
}
