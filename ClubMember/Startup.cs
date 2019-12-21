using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClubMember.Startup))]
namespace ClubMember
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
