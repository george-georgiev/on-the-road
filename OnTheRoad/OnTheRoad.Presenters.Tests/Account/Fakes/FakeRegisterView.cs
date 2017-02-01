using Microsoft.Owin;
using System;
using OnTheRoad.Account.Interfaces;
using OnTheRoad.EventArgsClasses;
using OnTheRoad.Models;

namespace OnTheRoad.Presenters.Tests.Account.Fakes
{
    public class FakeRegisterView : IRegisterView
    {
        public IOwinContext httpContext;

        public string subscribedMethod;

        public string parameterClassName;

        public RegisterModel Model { get; set; }

        public bool ThrowExceptionIfNoPresenterBound { get; }

        public event EventHandler Load;

        public event EventHandler<RegisterEventArgs> createUser;

        public event EventHandler<RegisterEventArgs> CreateUser
        {
            add
            {
                this.subscribedMethod = value.Method.Name;
                var parameters = value.Method.GetParameters();
                parameterClassName = parameters[1].ParameterType.Name;
                createUser += value;
            }
            remove
            {
                createUser -= value;
            }
        }

        public void InvokeGetCreateUser(RegisterEventArgs e)
        {
            this.createUser?.Invoke(null, e);
        }

        public void InvokeLoad()
        {
            this.Load?.Invoke(null, null);
        }
    }
}
