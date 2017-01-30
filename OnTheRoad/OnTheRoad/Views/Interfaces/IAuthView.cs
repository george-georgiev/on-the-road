using System;
using OnTheRoad.Models;
using WebFormsMvp;
using OnTheRoad.Views.EventArgsClasses;

namespace OnTheRoad.Views.Interfaces
{
    public interface IAuthView : IView<AuthModel>
    {
        event EventHandler<AuthEventArgs> CreateUser;
    }
}
