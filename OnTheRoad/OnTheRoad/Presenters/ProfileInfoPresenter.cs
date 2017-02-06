using OnTheRoad.Profile.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFormsMvp;

namespace OnTheRoad.Presenters
{
    public class ProfileInfoPresenter : Presenter<IProfileInfoView>
    {

        public ProfileInfoPresenter(IProfileInfoView view)
            : base(view)
        {
        }
    }
}