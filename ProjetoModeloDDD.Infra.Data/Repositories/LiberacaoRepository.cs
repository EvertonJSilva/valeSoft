using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Repositories;
using ProjetoModeloDDD.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoModeloDDD.Infra.Data.Repositories
{
    public class LiberacaoRepository : RepositoryBase<Liberacao>, ILiberacaoRepository
    {
        public IEnumerable<Liberacao> GetPorIdProfissional(int id)
        { 
            return Db.Liberacoes.Where(p => p.ProfissionalId == id);
        }
    }
}
