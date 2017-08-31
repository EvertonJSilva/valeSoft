
using AutoMapper;
using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.MVC.ViewModels;

namespace ProjetoModeloDDD.MVC.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Cliente, ClienteViewModel>();
            Mapper.CreateMap<Produto, ProdutoViewModel>();    
            Mapper.CreateMap<Usuario, LoginViewModel>();
            Mapper.CreateMap<Consulta, ConsultaViewModel>();
            Mapper.CreateMap<Liberacao, LiberacaoViewModel>();
            Mapper.CreateMap<Paciente, PacienteViewModel>();
            Mapper.CreateMap<Profissional, ProfissionalViewModel>();
            Mapper.CreateMap<Producao, ProducaoViewModel>();
            Mapper.CreateMap<TipoProfissional, TipoProfissionalViewModel>();
            Mapper.CreateMap<TaxaDoacao, TaxaDoacaoViewModel>();
            Mapper.CreateMap<ValorConsulta, ValorConsultaViewModel>();
            Mapper.CreateMap<TaxaExtraProfissional, TaxaExtraProfissionalViewModel>();


        }
    }
}