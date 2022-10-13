using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace learning_center_tonic3.Tests
{
    public class AsAdminFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Email, "admin@admin.com"),
                    new Claim(ClaimTypes.Upn, "admin@admin.com"),
                    new Claim(ClaimTypes.NameIdentifier, "d03cdd80-e8e9-4fc5-a2dc-176e99bb326a"),
                    new Claim(ClaimTypes.Role, "Admin")
                }, "Admin"));

            await next();
        }
    }
}