using ProjetoModeloDDD.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ProjetoModeloDDD.Infra.Data.EntityConfig
{
    public class LiberacaoConfiguration : EntityTypeConfiguration<Liberacao>
    {
        public LiberacaoConfiguration()
        {
            HasKey(l => l.LiberacaoId);


             Property(l => l.NumeroLiberacao)
                .IsRequired();

            HasRequired(l => l.Paciente)
                .WithMany()
                .HasForeignKey(l => l.PacienteId);

        }
    }
}
