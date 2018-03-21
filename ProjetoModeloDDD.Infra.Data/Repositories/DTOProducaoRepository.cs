using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoModeloDDD.Infra.Data.Repositories
{
    public class DTOProducaoRepository : RepositoryBase<DTOProducao>, IDTOProducaoRepository
    {
        public DTOProducao GetPorConsultaID(int id)
        {
            return (
                    from p in Db.Producoes
                    join e in Db.Consultas on p.ConsultaId equals e.ConsultaId
                    where e.ConsultaId == id
                    select new DTOProducao()
                    {

                    }

                ).Single();
         }

        public IEnumerable<DTOProducao> GetListaPorData(DateTime datainicial, DateTime dataFinal, int idProfissional, string nomePaciente)
        {



            return (from p in Db.Producoes
                    join e in Db.Consultas on p.ConsultaId equals e.ConsultaId
                    join l in Db.Liberacoes on e.LiberacaoId equals l.LiberacaoId
                    join a in Db.Pacientes on l.PacienteId equals a.PacienteId
                    join i in Db.Profissionais on e.ProfissionalId equals i.ProfissionalId
                    where e.DataHoraConsulta >= datainicial && e.DataHoraConsulta <= dataFinal
                                && (a.NomePaciente.ToLower().Contains(nomePaciente))
                                && (idProfissional != 0
                                        ? p.Consulta.ProfissionalId == idProfissional
                                        : p.Consulta.ProfissionalId != 0)
                    select new DTOProducao()
                    {
                        nomePaciente = a.NomePaciente,
                        carteirinhaPaciente = a.CarteirinhaPaciente,
                        sessaoConsulta = e.TipoSessao,
                        valorConsulta = e.ValorConsulta,
                        dataInicial = datainicial,
                        dataFinal = dataFinal,
                        matricula = i.Matricula,
                        nomeProfissional = i.NomeProfissional,
                        dataIngresso = i.DataIngresso,
                        INSS = i.INSS,
                        CPF = i.Cpf,
                        valorCopart = e.ValorCopart,
                        valorConvenio = e.ValorConvenio,
                        taxaBancaria = i.TaxaBancaria,
                        copartPaciente = a.CopartPaciente,
                        revisado = p.revisado,
                        consolidado = p.Consolidado,
                        idProducao = p.ProducaoId,
                        profissionalId = i.ProfissionalId,
                        tipoProfissionalId = i.TipoProfissionalId,
                        numeroLiberacao = l.NumeroLiberacao,
                        autorizacao = e.Autorizacao,
                        dataConsulta = e.DataHoraConsulta,
                        pacienteId = a.PacienteId,
                        consultaId = e.ConsultaId
                        
                    
                    }
             ).ToList();

        }

     
    }
}


