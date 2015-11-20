namespace ImageGallery.Services.Data.Tests
{
    using System;
    using System.Reflection;
    using Api;
    using Common.Constants;
    using ImageGallery.Data.Contracts;
    using Fakes;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Ninject;

    [TestClass]
    public class TestInit
    {
        [AssemblyInitialize]
        public static void Init(TestContext testContext)
        {
            NinjectConfig.BindDbInterfaces = new Action<IKernel>(kernel =>
                {
                    kernel.Bind(typeof(IRepository<>)).To(typeof(FakeRepository<>));

                    kernel.Bind<IImageGalleryData>().To<FakeGalleryData>();
                });

            AutoMapperConfig.RegisterMappings(Assembly.Load(GlobalConstants.WebApiAssemblyName));
        }
    }
}