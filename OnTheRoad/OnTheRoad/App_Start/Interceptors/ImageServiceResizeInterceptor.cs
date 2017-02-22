using System;
using System.Web.Caching;
using Ninject.Extensions.Interception;
using OnTheRoad.Common;
using Ninject;
using OnTheRoad.Logic.Contracts;

namespace OnTheRoad.App_Start.Interceptors
{
    public class ImageServiceResizeInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
            var image = (byte[])invocation.ReturnValue;
            var imageResizer = invocation.Request.Kernel.Get<IImageResizer>();
            var resized = imageResizer.ResizeImage(image);
            invocation.ReturnValue = resized;
        }
    }
}