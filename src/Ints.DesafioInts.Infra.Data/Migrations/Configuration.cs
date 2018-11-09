using Ints.DesafioInts.Infra.Data.Context;

namespace Ints.DesafioInts.Infra.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DesafioIntsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DesafioIntsContext context)
        {

        }
    }
}
