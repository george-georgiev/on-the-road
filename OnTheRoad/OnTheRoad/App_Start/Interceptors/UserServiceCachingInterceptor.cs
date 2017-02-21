using System;
using System.Web.Caching;
using Ninject.Extensions.Interception;
using OnTheRoad.Common;

namespace OnTheRoad.App_Start.Interceptors
{
    public class UserServiceCachingInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var cache = CacheWrapper.Instance;
            var callingMethodName = invocation.Request.Method.Name;
            var cachedValue = cache[callingMethodName];
            if (cachedValue == null)
            {
                invocation.Proceed();
                cache.Insert(invocation.Request.Method.Name, invocation.ReturnValue, null, DateTime.Now.AddMinutes(5), Cache.NoSlidingExpiration);
            }
            else
            {
                invocation.ReturnValue = cachedValue;
            }
        }
    }
}