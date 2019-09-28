using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Hamro_dokan.Startup))]
namespace Hamro_dokan
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
