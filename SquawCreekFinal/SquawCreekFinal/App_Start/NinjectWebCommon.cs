[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SquawCreekFinal.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(SquawCreekFinal.App_Start.NinjectWebCommon), "Stop")]

namespace SquawCreekFinal.App_Start
{
    using System;
    using System.Web;
    using SqualCreekBusinessLayer.infacing;
    using SqualCreekBusinessLayer.concrete;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();
        private static IKernel _kernel = null;
        public static IKernel Kernel
        {
            get { return _kernel; } // bootstrapper.Kernel; 
        }
        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
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
            _kernel = kernel;
            kernel.Bind<ICommon>().To<Common>();
            kernel.Bind<IEvent>().To<Event>();
            kernel.Bind<ITeeTime>().To<TeeTimeBO>();
            kernel.Bind<IAdmin>().To<AdminBO>();
        }        
    }
}
