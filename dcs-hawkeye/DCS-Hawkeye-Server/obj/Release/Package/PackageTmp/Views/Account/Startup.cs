using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DCS_Hawkeye_Server.Startup))]
namespace DCS_Hawkeye_Server
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
