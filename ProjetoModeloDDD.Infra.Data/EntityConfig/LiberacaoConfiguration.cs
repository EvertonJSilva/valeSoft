using ProjetoModeloDDD.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ProjetoModeloDDD.Infra.Data.EntityConfig
{
    public class LiberacaoConfiguration : EntityTypeConfiguration<Liberacao>
    {
        public LiberacaoConfiguration()
        {
            HasKey(l => l.LiberacaoId);

             Property(f => f.NumeroLiberacao)
                .IsRequired();
                
       }
    }
}
