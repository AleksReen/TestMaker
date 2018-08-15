using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using TestMaker.Data.Context;
using TestMaker.Models.Data;

namespace TestMakerWebApp.Controllers
{
    public class TokenController : BaseApiController
    {
        public TokenController(
            ApplicationDbContext dbContext,        
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
            : base(dbContext, roleManager, userManager, configuration) {}

    }
}
