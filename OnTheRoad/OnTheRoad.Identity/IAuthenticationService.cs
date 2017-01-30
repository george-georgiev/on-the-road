using Microsoft.Owin;
using System;

namespace OnTheRoad.Identity
{
   public interface IAuthenticationService
    {
        void CreateUser(string username, string email, string password);
    }
}
