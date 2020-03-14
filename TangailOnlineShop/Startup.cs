using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TangailOnlineShop.Startup))]
namespace TangailOnlineShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
