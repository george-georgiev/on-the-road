using System;
using OnTheRoad.Models;
using WebFormsMvp;
using OnTheRoad.EventArgsClasses;

namespace OnTheRoad.Views.Interfaces
{
    public interface IRegisterView : IView<RegisterModel>
    {
        event EventHandler<RegisterEventArgs> CreateUser;
    }
}
