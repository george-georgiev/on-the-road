﻿using OnTheRoad.Data.Contracts;
using OnTheRoad.Data.Models;
using OnTheRoad.Data.Readers;
using System;
using System.Data.Entity.Migrations;

namespace OnTheRoad.Data.Seeders
{
    public class DataSeeder : IDataSeeder
    {
        private IDataReader dataReader;

        public DataSeeder()
        {
            this.DataReader = new TextDataReader();
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
                    throw new ArgumentNullException("dataReader can not be null!");
                }

                this.dataReader = value;
            }
        }

        public void SeedCategories(IOnTheRoadDbContext context)
        {
            var categories = this.DataReader.ReadCategories();
            foreach (var category in categories)
            {
                context.Categories.AddOrUpdate(new Category() { Name = category });
            }
        }

        public void SeedCities(IOnTheRoadDbContext context)
        {
            var cities = this.DataReader.ReadCities();
            foreach (var city in cities)
            {
                context.Cities.AddOrUpdate(new City() { Name = city });
            }
        }

        public void SeedRating(IOnTheRoadDbContext context)
        {
            var ratings = this.DataReader.ReadRatings();
            foreach (var rating in ratings)
            {
                context.Ratings.AddOrUpdate(new Rating() { Value = rating });
            }
        }
    }
}