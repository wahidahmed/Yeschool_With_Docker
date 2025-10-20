using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace School.AdminService.Helpers
{
    public class PermissionRequirement : IAuthorizationRequirement { }

    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            HttpContext httpContext = null;

            // Case 1: Direct HttpContext (common in API projects behind gateway)
            if (context.Resource is HttpContext h)
            {
                httpContext = h;
            }
            // Case 2: MVC Filter Context (if used in full MVC pipeline)
            else if (context.Resource is AuthorizationFilterContext mvcContext)
            {
                httpContext = mvcContext.HttpContext;
            }
            else
            {
                context.Fail();
                return Task.CompletedTask;
            }

            if (httpContext == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var endpoint = httpContext.GetEndpoint();
            if (endpoint?.Metadata == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            // Extract Controller/Action info
            var descriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
            if (descriptor == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var area = descriptor.ControllerTypeInfo.GetCustomAttribute<AreaAttribute>()?.RouteValue ?? "";
            var controller = descriptor.ControllerName;
            var action = descriptor.ActionName;

            // Normalize for case-insensitive comparison
            var requiredPermission = $"{area}:{controller}:{action}".ToLowerInvariant();

            var hasPermission = context.User.Claims
                .Where(c => c.Type.Equals("permission", StringComparison.OrdinalIgnoreCase))
                .Any(c => c.Value.Equals(requiredPermission, StringComparison.OrdinalIgnoreCase));

            if (hasPermission)
            {
                context.Succeed(requirement);
            }
            else
            {
                // Optional: Log missing permission
                // Console.WriteLine($"Missing permission: {requiredPermission}");
                context.Fail();
            }

            return Task.CompletedTask;
            //var httpContext = (context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext)?.HttpContext;
            //if (httpContext == null)
            //{
            //    context.Fail();
            //    return Task.CompletedTask;
            //}

            //var endpoint = httpContext.GetEndpoint();
            //var descriptor = endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>();
            //if (descriptor == null)
            //{
            //    context.Fail();
            //    return Task.CompletedTask;
            //}

            //var area = descriptor.ControllerTypeInfo.GetCustomAttribute<AreaAttribute>()?.RouteValue ?? "";
            //var controller = descriptor.ControllerName;
            //var action = descriptor.ActionName;

            //// Build the permission string
            //var requiredPermission = $"{area}:{controller}:{action}";

            //// Check if user has this permission claim
            //var hasPermission = context.User.Claims
            //    .Where(c => c.Type == "permission")
            //    .Any(c => c.Value.Equals(requiredPermission, StringComparison.OrdinalIgnoreCase));

            //if (hasPermission)
            //    context.Succeed(requirement);
            //else
            //    context.Fail();

            //return Task.CompletedTask;
        }
    }
}
