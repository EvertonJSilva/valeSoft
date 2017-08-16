using ProjetoModeloDDD.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ProjetoModeloDDD.Infra.Data.EntityConfig
{
    public class TaxaExtraProfissionalConfiguration : EntityTypeConfiguration<TaxaExtraProfissional>
    {
        public TaxaExtraProfissionalConfiguration()
        {
            HasKey(t => t.TaxaExtraProfissionalId);

            HasRequired(p => p.Profissional)
           .WithMany()
           .HasForeignKey(p => p.ProfissionalId);

            Property(p => p.dataCompensar)
            .HasColumnType("Datetime2")
            .IsRequired();

            Property(p => p.dataInsercao)
            .HasColumnType("Datetime2")
            .IsRequired();

            Property(p => p.tipo)
            .IsRequired();

            Property(p => p.valor)
            .IsRequired();

        }
    }
}
