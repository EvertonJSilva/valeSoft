
using AutoMapper;
using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.MVC.ViewModels;

namespace ProjetoModeloDDD.MVC.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<ClienteViewModel, Cliente>();
            Mapper.CreateMap<ProdutoViewModel, Produto>();
            Mapper.CreateMap<LoginViewModel, Usuario>();
            Mapper.CreateMap<ConsultaViewModel, Consulta>();
            Mapper.CreateMap<LiberacaoViewModel, Liberacao>();
            Mapper.CreateMap<PacienteViewModel, Paciente>();
            Mapper.CreateMap<ProfissionalViewModel, Profissional>();
            Mapper.CreateMap<ProducaoViewModel, Producao>();
            Mapper.CreateMap<TipoProfissionalViewModel, TipoProfissional>();
            Mapper.CreateMap<TaxaDoacaoViewModel, TaxaDoacao>();
            Mapper.CreateMap<ValorConsultaViewModel, ValorConsulta>();
            Mapper.CreateMap<TaxaExtraProfissionalViewModel, TaxaExtraProfissional>();



        }
    }
}