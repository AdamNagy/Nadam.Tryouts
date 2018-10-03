using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebFormsLearning.Startup))]
namespace WebFormsLearning
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
