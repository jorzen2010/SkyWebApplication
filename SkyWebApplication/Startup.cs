using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SkyWebApplication.Startup))]
namespace SkyWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
