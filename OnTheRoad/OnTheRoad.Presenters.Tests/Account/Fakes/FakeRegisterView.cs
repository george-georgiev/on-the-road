using System;
using OnTheRoad.Account.Contracts;
using OnTheRoad.EventArgsClasses;
using OnTheRoad.Models;

namespace OnTheRoad.Presenters.Tests.Account.Fakes
{
    public class FakeRegisterView : IRegisterView
    {
        public event EventHandler<RegisterEventArgs> CreateUserCustomEvent;

        public event EventHandler<RegisterEventArgs> CreateUser
        {
            add
            {
                this.SubscribedMethod = value.Method.Name;
                var parameters = value.Method.GetParameters();
                this.ParameterClassName = parameters[1].ParameterType.Name;
                this.CreateUserCustomEvent += value;
            }

            remove
            {
                this.CreateUserCustomEvent -= value;
            }
        }

        public event EventHandler Load;

        public string SubscribedMethod { get; set; }

        public string ParameterClassName { get; set; }

        public RegisterModel Model { get; set; }

        public bool ThrowExceptionIfNoPresenterBound { get; }

        public void InvokeGetCreateUser(RegisterEventArgs e)
        {
            this.CreateUserCustomEvent?.Invoke(null, e);
        }

        public void InvokeLoad()
        {
            this.Load?.Invoke(null, null);
        }
    }
}
