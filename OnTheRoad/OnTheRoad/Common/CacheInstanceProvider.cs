using System;
using System.Web.Caching;

namespace OnTheRoad.Common
{
    internal class CacheInstanceProvider
    {
        private static volatile Cache instance;
        private static object syncRoot = new Object();

        private CacheInstanceProvider()
        {

        }

        internal static Cache Instance
        {
            get
            {
                return instance;
            }

            set
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = value;
                        }
                    }
                }
            }
        }
    }
}