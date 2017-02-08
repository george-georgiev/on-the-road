using OnTheRoad.Mvp.EventArgsClasses;
using OnTheRoad.Mvp.Models;
using System;
using WebFormsMvp;

namespace OnTheRoad.Mvp.Account.Contracts
{
    public interface ILoginView : IView<LoginModel>
    {
        event EventHandler<LoginEventArgs> LoginUser;
    }
}
