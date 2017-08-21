using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Repositories;
using ProjetoModeloDDD.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoModeloDDD.Domain.Services
{
    public class LiberacaoService : ServiceBase<Liberacao>, ILiberacaoService
    {

        private readonly ILiberacaoRepository _liberacaoRepository;
        private readonly IConsultaRepository _consultaRepository;


        public LiberacaoService(ILiberacaoRepository liberacaoRepository, IConsultaRepository consultaRepository)
            : base(liberacaoRepository)
        {
            _liberacaoRepository = liberacaoRepository;
            _consultaRepository = consultaRepository;
        }

        public void AtualizarConsultasRealizadas(Liberacao liberacao)
        {

            IEnumerable<Consulta> consultas = _consultaRepository.GetAll()
                .Where(c => c.LiberacaoId == liberacao.LiberacaoId)
                .Where( c => c.Status != "Pré-agendado");

            var liberacaoDomain = _liberacaoRepository.GetById(liberacao.LiberacaoId);

            liberacaoDomain.QuantidadeRealizada = consultas.Count() + liberacao.QuantidadeRealizadaExterno;

            _liberacaoRepository.Update(liberacaoDomain);
            
        }
    }
}
