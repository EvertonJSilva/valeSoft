using ProjetoModeloDDD.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ProjetoModeloDDD.Infra.Data.EntityConfig
{
    public class ConsultaConfiguration : EntityTypeConfiguration<Consulta>
    {
        public ConsultaConfiguration()
        {
            HasKey(c => c.ConsultaId);

            Property(c => c.Autorizacao)
                .HasMaxLength(70);
            //.IsRequired();

            Property(c => c.FormaAutorizar)
                .HasMaxLength(70);
            //.IsRequired();

            Property(c => c.Status)
                .HasMaxLength(20);

            Property(c => c.TipoSessao)
                .HasMaxLength(20);
            //.IsRequired();

            //Property(c => c.DataHoraConsulta);
            //Property(c => c.ValorConsulta);
            //Property(c => c.ValorConvenio);
            //Property(c => c.ValorCopart);
            //Property(c => c.DataCadastro);

            HasRequired(c => c.Profissional)
             .WithMany()
             .HasForeignKey(c => c.ProfissionalId);

            HasRequired(c => c.Liberacao)
             .WithMany()
             .HasForeignKey(c => c.LiberacaoId);


        }
        
    }
}
