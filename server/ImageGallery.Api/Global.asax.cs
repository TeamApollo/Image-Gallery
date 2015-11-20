namespace ImageGallery.Api
{
    using System.Reflection;
    using System.Web.Http;
    using Common.Constants;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {            
            AutoMapperConfig.RegisterMappings(Assembly.Load(GlobalConstants.WebApiAssemblyName));
            DatabaseConfig.Initialize();
        }
    }
}
