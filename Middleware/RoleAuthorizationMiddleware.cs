using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Grupal.Middleware
{
    // Simple middleware that checks for a header or session value "X-User-Role" and validates allowed roles
    public class RoleAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public RoleAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // This middleware only demonstrates role checking; for production use ASP.NET Core Identity/Authorization policies.
            // If an endpoint sets an item "RequiredRoles" (string[]) in the Items collection, validate it here.
            if (context.Items.TryGetValue("RequiredRoles", out var obj) && obj is string[] requiredRoles && requiredRoles.Length > 0)
            {
                string role = null;
                if (context.Request.Headers.TryGetValue("X-User-Role", out var vals))
                {
                    role = vals.FirstOrDefault();
                }

                // Fallback to session (if configured)
                if (role == null && context.Session != null && context.Session.TryGetValue("UserRole", out var _))
                {
                    // session value handling requires serialization/deserialization; keeping simple here
                    // Assume session contains plain string in a real implementation
                }

                if (role == null || !requiredRoles.Contains(role, StringComparer.OrdinalIgnoreCase))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Forbidden: role required");
                    return;
                }
            }

            await _next(context);
        }
    }

    public static class RoleAuthorizationMiddlewareExtensions
    {
        public static IApplicationBuilder UseRoleAuthorization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RoleAuthorizationMiddleware>();
        }
    }
}
