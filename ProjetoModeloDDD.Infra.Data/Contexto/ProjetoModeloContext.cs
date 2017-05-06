using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Infra.Data.EntityConfig;

namespace ProjetoModeloDDD.Infra.Data.Contexto
{
    public class ProjetoModeloContext : DbContext
    {
        public ProjetoModeloContext()
            : base("ProjetoModeloDDD")
        {
            
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Folha> Folhas { get; set; }
        public DbSet<FolhaDetalhe> FolhaDetalhes { get; set; }
        public DbSet<Liberacao> Liberacoes { get; set; }
        public DbSet<Profissional> Profissionais { get; set; }
        public DbSet<TaxaDoacao> TaxaDoacoes { get; set; }
        public DbSet<ValorConsulta> ValorConsultas { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties()
                .Where(p => p.Name == p.ReflectedType.Name + "Id")
                .Configure(p => p.IsKey());

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));

            modelBuilder.Configurations.Add(new ClienteConfiguration());
            modelBuilder.Configurations.Add(new ProdutoConfiguration());
            modelBuilder.Configurations.Add(new FolhaConfiguration());
            modelBuilder.Configurations.Add(new FolhaDetalheConfiguration());
            modelBuilder.Configurations.Add(new LiberacaoConfiguration());
            modelBuilder.Configurations.Add(new PacienteConfiguration());
            modelBuilder.Configurations.Add(new ProfissionalConfiguration());
            modelBuilder.Configurations.Add(new TaxaDoacaoConfiguration());
            modelBuilder.Configurations.Add(new ValorConsultaConfiguration());
      

        }

       public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
           {
                if (entry.State == EntityState.Added)
               {
                  entry.Property("DataCadastro").CurrentValue = DateTime.Now;
              }

              if (entry.State == EntityState.Modified)
             {
                   entry.Property("DataCadastro").IsModified = false;
                }
            }
         return base.SaveChanges();
     }
    }
}
