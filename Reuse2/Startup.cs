using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Reuse2.Startup))]
namespace Reuse2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
