[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(OnTheRoad.MVC.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(OnTheRoad.MVC.App_Start.NinjectWebCommon), "Stop")]

namespace OnTheRoad.MVC.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using System.Reflection;
    using System.Linq;
    using Ninject.Modules;
    internal static class NinjectWebCommon 
    {
        private const string ModuleClassSuffix = "Module";
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        internal static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        internal static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);

                // Make IKernel instance available.
                NinjectKernelInstanceProvider.Instance = kernel;

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            Assembly.GetAssembly(typeof(MvcApplication))
                .GetTypes()
                .Where(type => (typeof(NinjectModule)).IsAssignableFrom(type) && type.Name.Contains(ModuleClassSuffix))
                .Select(type => (INinjectModule)Activator.CreateInstance(type))
                .ToList()
                .ForEach(instance => kernel.Load(instance));
        }        
    }
}
