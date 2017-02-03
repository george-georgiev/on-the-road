using System;
using WebFormsMvp;
using OnTheRoad.Models;
using OnTheRoad.EventArgsClasses;

namespace OnTheRoad.Account.Contracts
{
    public interface IRegisterView : IView<RegisterModel>
    {
        event EventHandler<RegisterEventArgs> CreateUser;
    }
}
