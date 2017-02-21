using System;
using System.Web.Caching;

namespace OnTheRoad.Common
{
    public class CacheWrapper
    {
        private static volatile Cache instance;
        private static object syncRoot = new Object();

        private CacheWrapper()
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