using System;
using System.Data.Entity.Migrations;
using System.Linq;
using OnTheRoad.Data.Contracts;
using OnTheRoad.Data.Seeders;

namespace OnTheRoad.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<OnTheRoadIdentityDbContext>
    {
        private IDataSeeder dataSeeder;

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

            this.DataSeeder = new DataSeeder();
        }

        public IDataSeeder DataSeeder
        {
            get
            {
                return this.dataSeeder;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("dataReader can not be null!");
                }

                this.dataSeeder = value;
            }
        }

        protected override void Seed(OnTheRoadIdentityDbContext context)
        {
            if (context.Categories.Count() == 0)
            {
                this.dataSeeder.SeedCategories(context);
            }

            if(context.Cities.Count() == 0)
            {
                this.dataSeeder.SeedCities(context);
            }

            if (context.Ratings.Count() == 0)
            {
                this.dataSeeder.SeedRating(context);
            }

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
        }
    }
}
