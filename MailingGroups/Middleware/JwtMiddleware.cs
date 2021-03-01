using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MailingGroups.Middleware
{
    public class JwtMiddleware
    {
        private readonly IConfiguration _configuration;
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context, UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                await AttachUserToContext(context, userManager, roleManager, signInManager, token);

            await _next(context);
        }

        private async Task<bool> AttachUserToContext(HttpContext context, UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager, string token)
        {
            try
            {
                var secretBytes = Encoding.UTF8.GetBytes(JwtConstants.Secret);

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = new SymmetricSecurityKey(secretBytes);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;

                var user = await userManager.FindByNameAsync(userId);
                await signInManager.SignInAsync(user, isPersistent: false);

                var roles = roleManager.Roles.Select(x => x.Name).ToArray();

                var contextUser = new GenericPrincipal(new ClaimsIdentity(user.UserName), roles);
                context.User = contextUser;
            }
            catch (Exception ex)
            {

            }
            return true;
        }
    }
}
