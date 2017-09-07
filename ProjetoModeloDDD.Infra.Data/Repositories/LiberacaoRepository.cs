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

        public IEnumerable<Liberacao> GetPorIdProfissional(int idProfissional, string nomePaciente, string numeroliberacao)
        {
            //return  Db.Consultas.Where(p => p.ProfissionalId == id
            //                                      );

            return (from e in Db.Consultas
                    join l in Db.Liberacoes on e.LiberacaoId equals l.LiberacaoId
                    join a in Db.Pacientes on l.PacienteId equals a.PacienteId
                    where (a.NomePaciente.ToLower().Contains(nomePaciente))
                                && (l.NumeroLiberacao.ToLower().Contains(numeroliberacao))
                                && (idProfissional != 0
                                        ? e.ProfissionalId == idProfissional
                                        : e.ProfissionalId != 0)
                    select l
             );

        }
    }
}