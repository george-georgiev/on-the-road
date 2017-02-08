using System;
using WebFormsMvp.Web;
using WebFormsMvp;
using OnTheRoad.Mvp.Presenters;
using OnTheRoad.Mvp.Models;
using OnTheRoad.Mvp.CustomControllers.Contracts;
using OnTheRoad.Mvp.EventArgsClasses;

namespace OnTheRoad.CustomControllers
{
    [PresenterBinding(typeof(CategoryOverviewPresenter))]
    public partial class CategoryOverview : MvpUserControl<CategoryOverviewModel>, ICategoryOverviewView
    {
        public string CategoryName { get; set; }

        public event EventHandler<CategoryOverviewEventArgs> OnPageLoad;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.OnPageLoad?.Invoke(this, new CategoryOverviewEventArgs() { CategoryName = this.CategoryName });
        }
    }
}