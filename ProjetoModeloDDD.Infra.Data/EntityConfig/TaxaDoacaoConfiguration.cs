using ProjetoModeloDDD.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ProjetoModeloDDD.Infra.Data.EntityConfig
{
    public class TaxaDoacaoConfiguration : EntityTypeConfiguration<TaxaDoacao>
    {
        public TaxaDoacaoConfiguration()
        {
            HasKey(t => t.TaxaDoacaoId);

            HasRequired(p => p.TipoProfissional)
           .WithMany()
           .HasForeignKey(p => p.TipoProfissionalId);

        }
    }
}
