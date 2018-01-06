using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Pranzo.Site.Startup))]
namespace Pranzo.Site
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
