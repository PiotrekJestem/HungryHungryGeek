using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HungryHungryGeek.Startup))]
namespace HungryHungryGeek
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
