using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BasicAPI.Startup))]

namespace BasicAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
