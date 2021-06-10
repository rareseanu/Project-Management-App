using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using ProjectManagementApp.Services;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementApp.Filters
{
    public class LoggedInTodayFilter : IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            string token = context.HttpContext.Request.Headers["Authorization"];
            token = token?.Replace("Bearer", "").Trim();

            if (token != null)
            {
                var tokenHandler = (JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(token);
                using var claims = tokenHandler.Claims.GetEnumerator();

                while (claims.MoveNext())
                {
                    if (claims.Current?.Type == ClaimTypes.NameIdentifier)
                    {
                        var userId = claims.Current.Value;
                        var userService = context.HttpContext.RequestServices.GetService<UserService>();

                        var dbUser = await userService
                            .Get(p => p.Id.ToString() == userId)
                            .FirstOrDefaultAsync();
                        if (dbUser.RefreshTokenExpires.HasValue)
                        {
                            var lastLogin = dbUser.RefreshTokenExpires.Value;
                            if(lastLogin.Date == DateTime.Now.Date)
                            {
                                throw new BadHttpRequestException("User logged in today.");
                            }
                        }
                    }
                }
            }
        }
    }
}
