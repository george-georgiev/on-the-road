using OnTheRoad.Mvp.EventArgsClasses;
using OnTheRoad.Mvp.Models;
using System;
using WebFormsMvp;

namespace OnTheRoad.Mvp.Views
{
    public interface ICategoriesView : IView<CategoriesModel>
    {
        event EventHandler GetCategories;

        event EventHandler<CategoriesEventArgs> GetTrips;

        event EventHandler<CategoriesEventArgs> GetTripsTotalCount;
    }
}
