namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApi.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApi.Models.ApplicationDbContext context)
        {
            context.Roles.AddOrUpdate(
                r => r.Name,
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Name = "Admin" },
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Name = "User" }
            );
        }
    }
}
