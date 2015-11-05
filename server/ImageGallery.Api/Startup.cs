using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ImageGallery.Api.Startup))]

namespace ImageGallery.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
