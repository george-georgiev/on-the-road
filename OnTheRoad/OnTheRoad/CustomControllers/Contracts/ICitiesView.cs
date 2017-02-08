using System;
using OnTheRoad.Models;
using WebFormsMvp;

namespace OnTheRoad.CustomControllers.Contracts
{
    public interface ICitiesView : IView<CitiesModel>
    {
        event EventHandler GetCities;
    }
}
