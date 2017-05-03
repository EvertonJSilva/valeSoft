using ProjetoModeloDDD.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ProjetoModeloDDD.Infra.Data.EntityConfig
{
    public class FolhaDetalheConfiguration : EntityTypeConfiguration<FolhaDetalhe>
    {
        public FolhaDetalheConfiguration() {

            HasKey(f => f.FolhaDetalheId);

            HasRequired(f => f.Folha)
                .WithMany()
                .HasForeignKey(f => f.FolhaId);

            Property(f => f.Descricao)
                .IsRequired()
                .HasMaxLength(150);
    }

    }
}
