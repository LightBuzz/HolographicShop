using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(HolographicShop.Startup))]

namespace HolographicShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}