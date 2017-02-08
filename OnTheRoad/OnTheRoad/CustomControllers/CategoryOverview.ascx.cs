using OnTheRoad.CustomControllers.Contracts;
using System;
using WebFormsMvp.Web;
using OnTheRoad.EventArgsClasses;
using OnTheRoad.Models;
using OnTheRoad.Presenters;
using WebFormsMvp;

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