using ProjetoModeloDDD.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ProjetoModeloDDD.Infra.Data.EntityConfig
{
    public class ProducaoConfiguration : EntityTypeConfiguration<Producao>
    {

        public ProducaoConfiguration()
        {
            HasKey(p => p.ProducaoId);
       
            Property(p => p.CarteirinhaPaciente)
               .IsRequired()
               .HasMaxLength(150);

            HasRequired(l => l.Consulta)
             .WithMany()
             .HasForeignKey(l => l.ConsultaId);

            Property(p => p.dataInicial)
                .IsOptional()
                .HasColumnType("datetime2");

            Property(p => p.dataFinal)
                .IsOptional()
                .HasColumnType("datetime2");


        }

    }
}
