using ProjetoModeloDDD.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ProjetoModeloDDD.Infra.Data.EntityConfig
{
    public class ValorConsultaConfiguration : EntityTypeConfiguration<ValorConsulta>
    {
        public ValorConsultaConfiguration()
        {

            HasKey(v => v.ValorConsultaId);

        }
    }
}
