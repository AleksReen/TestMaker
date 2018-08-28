using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TestMaker.Data.Context;
using TestMaker.Data.Processor.Providers;
using TestMaker.Models.Data;
using TestMaker.Models.ViewModels;

namespace TestMakerWebApp.Controllers
{
    public class UserController : BaseApiController
    {
        private IUserProvider dataProcessor;
        public UserController(
            ApplicationDbContext dbContext,
            IUserProvider DataProcessor,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
            : base(dbContext, roleManager, userManager, configuration)
        {
            dataProcessor = DataProcessor;
        }

        [HttpPost()]
        public async Task<IActionResult> Add([FromBody]UserViewModel model)
        {
            if (model == null)
            {
                return new StatusCodeResult(500);
            }

            ApplicationUser user = await UserManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                return BadRequest("User already exists");
            }

            user = await UserManager.FindByEmailAsync(model.Email);

            if (user != null) return BadRequest("Email already exists.");

            var now = DateTime.Now;

            user = new ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Email = model.Email,
                DisplayName = model.DisplayName,
                CreatedDate = now,
                LastModifiedDate = now
            };

            await UserManager.CreateAsync(user, model.Password);

            user.EmailConfirmed = true;
            user.LockoutEnabled = false;

            dataProcessor.Save(DbContext);

            return Json(user.Adapt<UserViewModel>(), JsonSettings);

        }
    }
}