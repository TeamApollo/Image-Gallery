namespace ImageGallery.Api
{
    using System;
    using System.Web;
    using Data;
    using Data.Contracts;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Web.Common;

    public static class NinjectConfig
    {
        //private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        ///// <summary>
        ///// Starts the application
        ///// </summary>
        //public static void Start()
        //{
        //    DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
        //    DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
        //    Bootstrapper.Initialize(CreateKernel);
        //}

        ///// <summary>
        ///// Stops the application.
        ///// </summary>
        //public static void Stop()
        //{
        //    Bootstrapper.ShutDown();
        //}

        public static Action<IKernel> BindDbInterfaces = kernel =>
        {
            kernel.Bind(typeof(IRepository<>)).To(typeof(GenericRepository<>));

            kernel.Bind<IImageGalleryData>().To<ImageGalleryData>();
        };

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        public static IKernel CreateKernel()
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
        public static void RegisterServices(IKernel kernel)
        {
            BindDbInterfaces(kernel);

            kernel
                .Bind<IImageGalleryDbContext>()
                .To<ImageGalleryDbContext>()
                .InRequestScope();



            // This will bind all classes to their interfaces if they have the same name
            // without the leading "I". For ex. Products : IProducts, Cats : ICats.
            kernel.Bind(b => b.From("ImageGallery.Services.Data")
                .SelectAllClasses()
                .BindDefaultInterface());
        }
    }
}
