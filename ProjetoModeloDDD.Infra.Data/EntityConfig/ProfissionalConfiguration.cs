using ProjetoModeloDDD.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ProjetoModeloDDD.Infra.Data.EntityConfig
{
    public class ProfissionalConfiguration : EntityTypeConfiguration<Profissional>
    {
        public ProfissionalConfiguration()
        {
            HasKey(p => p.ProfissionalId);

        Property(p => p.NomeProfissional)
            .IsRequired()
            .HasMaxLength(150);

        Property(p => p.Login)
            .IsRequired()
            .HasMaxLength(30);

        Property(p => p.Senha)
            .IsRequired()
            .HasMaxLength(50);

        Property(p => p.TipoProfissional)
            .IsRequired();
        }
    }
}
