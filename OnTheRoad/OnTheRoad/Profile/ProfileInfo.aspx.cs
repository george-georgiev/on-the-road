using System;
using OnTheRoad.EventArgsClasses;
using OnTheRoad.Models;
using OnTheRoad.Presenters;
using OnTheRoad.Profile.Contracts;
using WebFormsMvp;
using WebFormsMvp.Web;
using System.Collections.Generic;

namespace OnTheRoad.Profile
{
    [PresenterBinding(typeof(ProfileInfoPresenter))]
    public partial class ProfileInfo : MvpPage<ProfileInfoModel>, IProfileInfoView
    {
        public event EventHandler<ProfileInfoEventArgs> GetProfileInfo;


        //public IEnumerable<ProfileInfoModel> UserProfileInfo { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.GetProfileInfo?.Invoke(this, new ProfileInfoEventArgs());
            //this.UserProfileInfo = new List<ProfileInfoModel>() { this.Model };
            this.FormViewProfileInfo.DataSource = new List<ProfileInfoModel>() { this.Model };
            this.FormViewProfileInfo.DataBind();

            
        }
    }
}