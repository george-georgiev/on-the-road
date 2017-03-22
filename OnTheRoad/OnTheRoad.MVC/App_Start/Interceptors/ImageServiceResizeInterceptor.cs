using Ninject.Extensions.Interception;
using Ninject;
using OnTheRoad.Logic.Contracts;

namespace OnTheRoad.MVC.App_Start.Interceptors
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