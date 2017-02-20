using OnTheRoad.Mvp.Models;
using OnTheRoad.Mvp.Presenters;
using OnTheRoad.Mvp.Views;
using System;
using System.Web.UI;
using WebFormsMvp;

namespace OnTheRoad
{
    [PresenterBinding(typeof(SiteNavigationPresenter))]
    public partial class SiteNavigation : MasterPage, ISiteNavigationView
    {
        public SiteNavigationModel Model { get; set; }

        public bool ThrowExceptionIfNoPresenterBound { get; }

        public event EventHandler<EventArgs> GetCategories;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.GetCategories?.Invoke(this, new EventArgs());
            this.ListViewCategories.DataSource = this.Model.Categories;
            this.ListViewCategories.DataBind();
        }

        protected void LinkButtonSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TextBoxSearch.Text) && !string.IsNullOrWhiteSpace(this.TextBoxSearch.Text))
            {
                this.Response.Redirect($"/trips/search/{this.TextBoxSearch.Text}");
            }
        }
    }
}