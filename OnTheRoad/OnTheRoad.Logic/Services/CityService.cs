using System;
using System.Collections.Generic;
using OnTheRoad.Domain.Models;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Domain.Repositories;

namespace OnTheRoad.Logic.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            if (cityRepository == null)
            {
                throw new ArgumentNullException("cityRepository cannot be null!");
            }

            this.cityRepository = cityRepository;
        }

        public IEnumerable<ICity> GetAllCities()
        {
            return this.cityRepository.GetAll();
        }

        public ICity GetCityById(int id)
        {
            return this.cityRepository.GetById(id);
        }
    }
}
