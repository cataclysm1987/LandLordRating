using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LandLordRating.Startup))]
namespace LandLordRating
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
