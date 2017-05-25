
using System.Data.Entity.ModelConfiguration;
using ProjetoModeloDDD.Domain.Entities;


namespace ProjetoModeloDDD.Infra.Data.EntityConfig
{
    class TipoProfissionalConfiguration : EntityTypeConfiguration<TipoProfissional>
    {
        public TipoProfissionalConfiguration()
        {
            HasKey(c => c.TipoProfissionalId);

            Property(c => c.Descricao)
                .IsRequired()
                .HasMaxLength(150);
        }
    }
}
