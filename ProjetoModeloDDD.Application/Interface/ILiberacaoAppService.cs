using ProjetoModeloDDD.Domain.Entities;
using System.Collections.Generic;

namespace ProjetoModeloDDD.Application.Interface
{
    public interface ILiberacaoAppService : IAppServiceBase<Liberacao>
    {

        void AtualizarConsultasRealizadas(Liberacao liberacao);

        IEnumerable<Liberacao> GetPorIdProfissional(int id);
    }
}
