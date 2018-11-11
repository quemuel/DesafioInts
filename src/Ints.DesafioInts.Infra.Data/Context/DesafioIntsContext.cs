using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Ints.DesafioInts.Domain.Entities;
using Ints.DesafioInts.Infra.Data.EntityConfig;

namespace Ints.DesafioInts.Infra.Data.Context
{
    public class DesafioIntsContext : DbContext
    {
        public DesafioIntsContext() : base("DefaultConnection")
        {
            //Database.SetInitializer<DesafioIntsContext>(new MigrateDatabaseToLatestVersion<DesafioIntsContext, Configuration>());
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<PorteEmpresa> PorteEmpresas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Configurations.Add(new ClienteConfig());
            modelBuilder.Configurations.Add(new PorteEmpresaConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}