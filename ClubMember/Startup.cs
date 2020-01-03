using ClubMember.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
            CreateUserAndRoles();
        }

        public void CreateUserAndRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //Create SuperAdmin Role
            if (!roleManager.RoleExists("SuperAdmin"))
            {
                var role = new IdentityRole("SuperAdmin");
                roleManager.Create(role);
            }

            //Create Default User
            var user = new ApplicationUser();
            user.UserName = "Admin@ClubMember.com";
            user.Email = "Admin@ClubMember.com";

            string pwd = "$pasWD567*";

            var defaultUser = userManager.Create(user, pwd);

            if(defaultUser.Succeeded)
            {
                userManager.AddToRole(user.Id, "SuperAdmin");
            }
        }
    }
}
