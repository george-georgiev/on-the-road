using OnTheRoad.Mvp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFormsMvp;

namespace OnTheRoad.Mvp.Presenters
{
    public class AddTripPresenter : Presenter<IAddTripView>
    {
        public AddTripPresenter(IAddTripView view) : base(view)
        {
        }
    }
}
