using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using NewGameStore.Models;


namespace NewGameStore.Migrations
{


    internal sealed class Configuration : DbMigrationsConfiguration<NewGameStore.DAL.StoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NewGameStore.DAL.StoreContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var ratings = new List<ESRB>
            {
                new ESRB {Rating = "RP", Description = "Rating Pending", Age = 0},
                new ESRB {Rating = "E", Description = "Everyone", Age = 3},
                new ESRB {Rating = "E10+", Description = "Everyone 10+", Age = 10},
                new ESRB {Rating = "T", Description = "Teen", Age = 13},
                new ESRB {Rating = "M", Description = "Mature", Age = 17},
                new ESRB {Rating = "AO", Description = "Adults Only", Age = 18}
            };
            ratings.ForEach(r => context.Ratings.AddOrUpdate(r));
            context.SaveChanges();
        }
    }
}
