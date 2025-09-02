using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
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
            var httpContext = (context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext)?.HttpContext;
            if (httpContext == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var endpoint = httpContext.GetEndpoint();
            var descriptor = endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>();
            if (descriptor == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var area = descriptor.ControllerTypeInfo.GetCustomAttribute<AreaAttribute>()?.RouteValue ?? "";
            var controller = descriptor.ControllerName;
            var action = descriptor.ActionName;

            // Build the permission string
            var requiredPermission = $"{area}:{controller}:{action}";

            // Check if user has this permission claim
            var hasPermission = context.User.Claims
                .Where(c => c.Type == "permission")
                .Any(c => c.Value.Equals(requiredPermission, StringComparison.OrdinalIgnoreCase));

            if (hasPermission)
                context.Succeed(requirement);
            else
                context.Fail();

            return Task.CompletedTask;
        }
    }
}
