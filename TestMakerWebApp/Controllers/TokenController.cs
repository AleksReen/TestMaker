using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Data.Context;
using TestMaker.Data.Processor.Providers;
using TestMaker.Models.Data;
using TestMaker.Models.ViewModels;

namespace TestMakerWebApp.Controllers
{
    public class TokenController : BaseApiController
    {
        ITokenProvider dataProcessor;

        public TokenController(
            ApplicationDbContext dbContext,        
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            ITokenProvider DataProcessor,
            IConfiguration configuration)
            : base(dbContext, roleManager, userManager, configuration)
        {
            dataProcessor = DataProcessor;
        }

        [HttpPost("Auth")]
        public async Task<IActionResult> Auth([FromBody]TokenRequestViewModel model)
        {
            // return a generic HTTP Status 500 (Server Error)
            // if the client payload is invalid.
            if (model == null) return new StatusCodeResult(500);

            switch (model.grant_type)
            {
                case "password":
                    return await GetToken(model);
                case "refresh_token":
                    return await RefreshToken(model);
                default:
                    // not supported - return a HTTP 401 (Unauthorized)
                    return new UnauthorizedResult();
            }
        }

        private async Task<IActionResult> RefreshToken(TokenRequestViewModel model)
        {
            try
            {
                var rt = dataProcessor.GetToken(this.DbContext, model);

                if (rt == null)
                {
                    return new UnauthorizedResult();
                }

                var userId = await UserManager.FindByNameAsync(rt.UserId);
               
                if (userId == null)
                {
                    return new UnauthorizedResult();
                }

                var rtNew = CreateRefreshToken(rt.ClientId, rt.UserId);

                dataProcessor.UpdateToken(this.DbContext, rt, rtNew);

                var response = CreateAccessToken(rtNew.UserId, rtNew.Value);

                return new JsonResult(response, JsonSettings);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private Token CreateRefreshToken(string clientId, string userId)
        {
            return new Token()
            {
                ClientId = clientId,
                UserId = userId,
                Type = 0,
                Value = Guid.NewGuid().ToString("N"),
                CreateDate = DateTime.UtcNow
            };
        }

        private TokenResponseViewModel CreateAccessToken(string userId, string refreshToken) {

            DateTime now = DateTime.UtcNow;

            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, userId),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeSeconds().ToString())};

            var tokenExpirationMins = Configuration.GetValue<int>("Auth:Jwt:TokenExpirationInMinutes");
            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Auth:Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: Configuration["Auth:Jwt:Issuer"],
                audience: Configuration["Auth:Jwt:Audience"],
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(tokenExpirationMins)),
                signingCredentials: new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256)
            );

            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
            // build & return the response
            return new TokenResponseViewModel()
            {
                token = encodedToken,
                expiration = tokenExpirationMins,
                refresh_token = refreshToken,
                userId = userId
            };

        }

        private async Task<IActionResult> GetToken(TokenRequestViewModel model)
        {
            try
            {

                // check if there's an user with the given username
                var user = await UserManager.FindByNameAsync(model.username);

                // fallback to support e-mail address instead of username
                if (user == null && model.username.Contains("@"))
                {
                    user = await UserManager.FindByEmailAsync(model.username);
                }

                if (user == null
                    || !await UserManager.CheckPasswordAsync(user, model.password))
                {
                    // user does not exists or password mismatch
                    return new UnauthorizedResult();
                }

                var rt = CreateRefreshToken(model.client_id, user.Id);

                dataProcessor.AddNewRefreshToken(DbContext, rt);

                var response = CreateAccessToken(user.Id, rt.Value);
               
                return new JsonResult(response, JsonSettings);

            }
            catch (Exception)
            {
                return new UnauthorizedResult();
            }
        }
    }
}
