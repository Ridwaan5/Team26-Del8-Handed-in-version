using EnginX.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(EnginX.Startup))]
namespace EnginX
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            create();
        }
        private void create()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            try
            {
                if (!roleManager.RoleExists("Admin"))
                {
                    var role = new IdentityRole();
                    role.Name = "Admin";
                    roleManager.Create(role);
                    var user = new ApplicationUser();
                    user.UserName = "SuperUser";
                    user.Email = "engenxpress@gmail.com";
                    user.EmailConfirmed = true;
                    user.PhoneNumber = "+27813419362";
                    user.PhoneNumberConfirmed = true;
                    string userPWD = "SuperUser123";
                    var chkUser = UserManager.Create(user, userPWD);
                    if (chkUser.Succeeded)
                    {
                        var result = UserManager.AddToRole(user.Id, "Admin");
                    }
                }
                if (!roleManager.RoleExists("Customer"))
                {
                    var role = new IdentityRole();
                    role.Name = "Customer";
                    roleManager.Create(role);
                }
                if (!roleManager.RoleExists("Cashier"))
                {
                    var role = new IdentityRole();
                    role.Name = "Cashier";
                    roleManager.Create(role);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
     }
}
