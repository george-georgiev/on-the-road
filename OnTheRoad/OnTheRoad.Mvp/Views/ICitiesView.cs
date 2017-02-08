using OnTheRoad.Mvp.Models;
using System;
using WebFormsMvp;

namespace OnTheRoad.Mvp.CustomControllers.Contracts
{
    public interface ICitiesView : IView<CitiesModel>
    {
        event EventHandler GetCities;
    }
}
