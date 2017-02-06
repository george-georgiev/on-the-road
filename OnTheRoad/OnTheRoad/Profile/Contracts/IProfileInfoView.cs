using System;
using OnTheRoad.EventArgsClasses;
using OnTheRoad.Models;
using WebFormsMvp;

namespace OnTheRoad.Profile.Contracts
{
    public interface IProfileInfoView : IView<ProfileInfoModel>
    { 
        event EventHandler<ProfileInfoEventArgs> GetProfileInfo;
    }
}
