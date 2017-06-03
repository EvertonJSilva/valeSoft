using ProjetoModeloDDD.Domain.Entities;

namespace ProjetoModeloDDD.Domain.Interfaces.Services
{
    public interface IProducaoService : IServiceBase<Producao>
    {
        Producao GetPorConsultaID(int id);
    }
}
