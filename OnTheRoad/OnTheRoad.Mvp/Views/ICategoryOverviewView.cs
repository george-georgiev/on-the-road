using OnTheRoad.Mvp.EventArgsClasses;
using OnTheRoad.Mvp.Models;
using System;
using WebFormsMvp;

namespace OnTheRoad.Mvp.CustomControllers.Contracts
{
    public interface ICategoryOverviewView : IView<CategoryOverviewModel>
    {
        event EventHandler<CategoryOverviewEventArgs> OnPageLoad;
    }
}
