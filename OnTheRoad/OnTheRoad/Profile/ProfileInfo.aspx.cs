using System;
using OnTheRoad.EventArgsClasses;
using OnTheRoad.Models;
using OnTheRoad.Presenters;
using OnTheRoad.Profile.Contracts;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace OnTheRoad.Profile
{
    [PresenterBinding(typeof(ProfileInfoPresenter))]
    public partial class ProfileInfo : MvpPage<ProfileInfoModel>, IProfileInfoView
    {
        public event EventHandler<ProfileInfoEventArgs> GetProfileInfo;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.GetProfileInfo?.Invoke(this, new ProfileInfoEventArgs());

            var firstName = this.Model.FirstName;
        }
    }
}