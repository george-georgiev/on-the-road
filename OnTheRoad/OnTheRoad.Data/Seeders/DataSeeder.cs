using System;
using OnTheRoad.Data.Common;
using OnTheRoad.Data.Contracts;
using OnTheRoad.Data.Models;
using OnTheRoad.Data.Readers;

namespace OnTheRoad.Data.Seeders
{
    public class DataSeeder : IDataSeeder
    {
        private IDataReader dataReader;
        private IAddOrUpdateHelper addOrUpdateHelper;

        public DataSeeder()
        {
            this.DataReader = new TextDataReader();
            this.AddOrUpdateHelper = new AddOrUpdateHelper();
        }

        public IDataReader DataReader
        {
            get
            {
                return this.dataReader;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("dataReader cannot be null!");
                }

                this.dataReader = value;
            }
        }

        public IAddOrUpdateHelper AddOrUpdateHelper
        {
            get
            {
                return this.addOrUpdateHelper;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("addOrUpdateHelper cannot be null!");
                }

                this.addOrUpdateHelper = value;
            }
        }

        public void SeedCategories(IOnTheRoadDbContext context)
        {
            var categories = this.DataReader.ReadCategories();
            foreach (var category in categories)
            {
                this.AddOrUpdateHelper.AddOrUpdateEntity<Category>(context, new Category() { Name = category });
            }
        }

        public void SeedCities(IOnTheRoadDbContext context)
        {
            var cities = this.DataReader.ReadCities();
            foreach (var city in cities)
            {
                this.AddOrUpdateHelper.AddOrUpdateEntity<City>(context, new City() { Name = city });
            }
        }

        public void SeedRating(IOnTheRoadDbContext context)
        {
            var ratings = this.DataReader.ReadRatings();
            foreach (var rating in ratings)
            {
                this.AddOrUpdateHelper.AddOrUpdateEntity<Rating>(context, new Rating() { Value = rating });
            }
        }
    }
}
