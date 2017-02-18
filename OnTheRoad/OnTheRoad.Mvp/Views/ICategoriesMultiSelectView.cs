using System;
using OnTheRoad.Mvp.Models;
using WebFormsMvp;

namespace OnTheRoad.Mvp.Views
{
    public interface ICategoriesMultiSelectView : IView<CategoriesMultiSelectModel>
    {
        event EventHandler GetCategories;
    }
}
