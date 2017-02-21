using Ninject;
using Ninject.Extensions.Interception;
using System;
using System.Web.Caching;

namespace OnTheRoad.App_Start.Interceptors
{
    public class TripServiceCachingInterceptor : IInterceptor

    {
        public void Intercept(IInvocation invocation)
        {
            var cache = invocation.Request.Kernel.Get<Cache>();
            var callingMethodName = invocation.Request.Method.Name;
            var cachedValue = cache[callingMethodName];
            if (cachedValue == null)
            {
                cache.Insert(invocation.Request.Method.Name, invocation.ReturnValue, null, DateTime.MaxValue, TimeSpan.FromMinutes(5));
                invocation.Proceed();
            }
            else
            {
                invocation.ReturnValue = cachedValue;
            }
        }
    }
}