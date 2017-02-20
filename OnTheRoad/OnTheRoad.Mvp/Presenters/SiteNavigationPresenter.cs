using OnTheRoad.Logic.Contracts;
using OnTheRoad.Mvp.Views;
using System;
using WebFormsMvp;

namespace OnTheRoad.Mvp.Presenters
{
    public class SiteNavigationPresenter : Presenter<ISiteNavigationView>
    {
        private readonly ICategoryService categoryService;

        public SiteNavigationPresenter(ISiteNavigationView view, ICategoryService categoryService) : base(view)
        {
            if (categoryService == null)
            {
                throw new ArgumentNullException("categoryService can not be null!");
            }

            this.categoryService = categoryService;

            this.View.GetCategories += View_GetCategories;
        }

        private void View_GetCategories(object sender, EventArgs e)
        {
            var categories = this.categoryService.GetAllCategories();
            this.View.Model.Categories = categories;
        }
    }
}
