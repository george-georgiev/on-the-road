using OnTheRoad.Logic.Models;
using OnTheRoad.Mvp.Views;
using System;
using System.Collections.Generic;
using WebFormsMvp;

namespace OnTheRoad.Mvp.Presenters
{
    public class CategoriesPresenter : Presenter<ICategoriesView>
    {
        public CategoriesPresenter(ICategoriesView view) : base(view)
        {
            this.View.GetCategories += View_GetCategories;
        }

        private void View_GetCategories(object sender, EventArgs e)
        {
            var categories = new List<Category>() { new Category("Archeology"), new Category("Nature") };
            this.View.Model.Categories = categories;
        }
    }
}
