using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectManagementApp.Helpers
{
    public static class HelperExtensionMethods
    {
        public static int GetUserId(this ClaimsPrincipal principal)
        {
            var userId = principal.Claims
                .FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return 0;

            return userId.ToInt();
        }

        public static string GetUserRole(this ClaimsPrincipal principal)
        {
            var role = principal.Claims
                .FirstOrDefault(p => p.Type == ClaimTypes.Role)?.Value;

            if (role == null)
                return null;

            return role;
        }

        public static int ToInt(this string obj)
        {
            return int.Parse(obj);
        }
    }
}
