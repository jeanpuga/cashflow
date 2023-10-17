using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Filter
{
    public class AuthorizeAttribute : ActionFilterAttribute
    {
        private readonly List<string> ExceptionRoutes = new() { "Health", "Auth", "Admin" };

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var routeValues = context.ActionDescriptor.RouteValues["controller"];
            var actionValues = context.ActionDescriptor.RouteValues["action"];

            if (ExceptionRoutes.Contains(routeValues) || ExceptionRoutes.Contains(actionValues))
            {
                await next();
            }
            else
            {
                if (context.HttpContext.Request.Headers.TryGetValue("Authorization", out var header))
                {
                    await next();
                }
                else
                {
                    Log.Error("[AuthorizeAttribute]Route {@routeValues} token broken - unauthorized", routeValues);
                    context.Result = new UnauthorizedObjectResult("unauthorized");
                }
            }
        }
    }
}