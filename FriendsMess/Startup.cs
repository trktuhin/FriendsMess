using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FriendsMess.Startup))]
namespace FriendsMess
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
