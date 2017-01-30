using System;
using OnTheRoad.Identity;

namespace OnTheRoad.App_Start
{
    public sealed class AuthConfiguration
    {
        private static volatile ConfigureAuthService instance;
        private static object syncRoot = new Object();

        private AuthConfiguration()
        {

        }

        public static ConfigureAuthService Instance
        {
            get
            {
                return AuthConfiguration.instance;
            }

            set
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            AuthConfiguration.instance = value;
                        }
                    }
                }
            }
        }
    }
}