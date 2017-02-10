using OnTheRoad.Mvp.Models;
using OnTheRoad.Mvp.Presenters;
using OnTheRoad.Mvp.Views;
using System;
using System.Collections.Generic;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace OnTheRoad.CustomControllers
{
    [PresenterBinding(typeof(CategoriesPresenter))]
    public partial class CategoriesMultiSelect : MvpUserControl<CategoriesModel>, ICategoriesView
    {
        public IEnumerable<int> SelecetedCategoryIds
        {
            get
            {
                var indices = this.CategoriesListBox.GetSelectedIndices();
                var selectedValues = new List<int>();
                foreach (var index in indices)
                {
                    var value = this.CategoriesListBox.Items[index].Value;
                    selectedValues.Add(int.Parse(value));
                }

                return selectedValues;
            }
        }

        public bool IsRequired { get; set; }

        public string ErrorMessage { get; set; }

        public event EventHandler GetCategories;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            this.GetCategories?.Invoke(this, new EventArgs());
            this.CategoriesListBox.DataSource = this.Model.Categories;
            this.CategoriesListBox.DataBind();
        }
    }
}