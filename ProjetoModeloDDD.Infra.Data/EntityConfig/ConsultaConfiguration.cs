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
                .IsRequired();
                

            
        }


    }
}
