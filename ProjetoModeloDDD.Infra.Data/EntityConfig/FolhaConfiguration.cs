using ProjetoModeloDDD.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ProjetoModeloDDD.Infra.Data.EntityConfig
{
    public class FolhaConfiguration : EntityTypeConfiguration<Folha>
    {
        public FolhaConfiguration(){
         HasKey(f => f.FolhaId);

            HasRequired(f => f.Profissional)
                .WithMany()
                .HasForeignKey(f => f.ProfissionalId);

        }
    }
}
