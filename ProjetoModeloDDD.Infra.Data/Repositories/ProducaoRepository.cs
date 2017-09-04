using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoModeloDDD.Infra.Data.Repositories
{
    public class ProducaoRepository : RepositoryBase<Producao>, IProducaoRepository
    {
        public Producao GetPorConsultaID(int id)
        {
          return   Db.Producoes.Where(p => p.Consulta.ConsultaId == id).Single();     
        }

        public IEnumerable<Producao> GetListaPorData(DateTime datainicial, DateTime dataFinal, int idProfissional, string nomePaciente)
        {

            //return Db.Producoes
            //    .Where(p => p.Consulta.DataHoraConsulta >= datainicial
            //                    && p.Consulta.DataHoraConsulta <= dataFinal
            //                    && p.Consulta.Liberacao.Paciente.NomePaciente.ToLower().Contains(nomePaciente)
            //                    && idProfissional != 0 
            //                            ? p.Consulta.ProfissionalId == idProfissional 
            //                            : p.Consulta.ProfissionalId != 0);


            return (from p in Db.Producoes
                    join e in Db.Consultas on p.ConsultaId equals e.ConsultaId
                    join l in Db.Liberacoes on e.LiberacaoId equals l.LiberacaoId
                    join a in Db.Pacientes on l.PacienteId equals a.PacienteId
                    where e.DataHoraConsulta >= datainicial && e.DataHoraConsulta <= dataFinal
                                && (a.NomePaciente.ToLower().Contains(nomePaciente))
                                && (idProfissional != 0
                                        ? p.Consulta.ProfissionalId == idProfissional
                                        : p.Consulta.ProfissionalId != 0)
                    select p
             );

        }
    }
}
