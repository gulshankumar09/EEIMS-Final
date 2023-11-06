using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EEIMS.Startup))]
namespace EEIMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
