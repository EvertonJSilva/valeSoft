using ProjetoModeloDDD.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ProjetoModeloDDD.Infra.Data.EntityConfig
{
    public class PacienteConfiguration : EntityTypeConfiguration<Paciente>
    {
        public PacienteConfiguration()
        {
            HasKey(p => p.PacienteId);

            Property(p => p.NomePaciente)
               .IsRequired()
               .HasMaxLength(150);

            Property(p => p.CarteirinhaPaciente)
               .IsRequired()
               .HasMaxLength(150);

        }
    }
}
