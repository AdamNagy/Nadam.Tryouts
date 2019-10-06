using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(POC_Authentication.Startup))]
namespace POC_Authentication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
