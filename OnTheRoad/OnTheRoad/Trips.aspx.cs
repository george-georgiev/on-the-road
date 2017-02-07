using OnTheRoad.Logic.Models;
using System;
using System.Collections.Generic;

namespace OnTheRoad
{
    public partial class Trips : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.CategoryRepeater.DataSource = new List<Category>() { new Category("Archeology"), new Category("Nature") };
            this.CategoryRepeater.DataBind();
        }
    }
}