using System;
using WebFormsMvp;
using OnTheRoad.EventArgsClasses;
using OnTheRoad.Models;

namespace OnTheRoad.Account.Contracts
{
    public interface ILoginView : IView<LoginModel>
    {
        event EventHandler<LoginEventArgs> LoginUser;
    }
}
