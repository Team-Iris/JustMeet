using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(JustMeet.Services.Startup))]

namespace JustMeet.Services
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
