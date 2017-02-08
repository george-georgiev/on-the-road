using OnTheRoad.EventArgsClasses;
using OnTheRoad.Models;
using System;
using WebFormsMvp;

namespace OnTheRoad.CustomControllers.Contracts
{
    public interface ICategoryOverviewView : IView<CategoryOverviewModel>
    {
        event EventHandler<CategoryOverviewEventArgs> OnPageLoad;
    }
}
