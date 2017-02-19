using OnTheRoad.Domain.Models;
using System;
using System.Web.UI;

namespace OnTheRoad.CustomControllers
{
    public partial class TripItem : UserControl
    {
        public ITrip  Trip{ get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}