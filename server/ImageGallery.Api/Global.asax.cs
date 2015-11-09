namespace ImageGallery.Api
{
    using System.Reflection;
    using System.Web.Http;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {            
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutoMapperConfig.RegisterMappings(Assembly.Load("ImageGallery.Api"));          
            DatabaseConfig.Initialize();
        }
    }
}
