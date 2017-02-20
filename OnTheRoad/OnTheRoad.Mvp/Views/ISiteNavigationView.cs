using OnTheRoad.Mvp.Models;
using System;
using WebFormsMvp;

namespace OnTheRoad.Mvp.Views
{
    public interface ISiteNavigationView : IView<SiteNavigationModel>
    {
        event EventHandler<EventArgs> GetCategories;
    }
}
