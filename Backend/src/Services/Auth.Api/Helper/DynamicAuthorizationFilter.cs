using Auth.Api.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Claims;

namespace Auth.Api.Helper
{
    public class DynamicAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly AppDbContext _dbContext;

        public DynamicAuthorizationFilter(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var controllerActionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
            var area = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttribute<AreaAttribute>()?.RouteValue;
            var controller = controllerActionDescriptor.ControllerName;
            var action = controllerActionDescriptor.ActionName;

            if (!IsProtectedAction(context))
                return;

            if (!IsUserAuthenticated(context))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var loggedInUserRoles = ((ClaimsIdentity)context.HttpContext.User.Identity).Claims
                                    .Where(c => c.Type == ClaimTypes.Role)
                                    .Select(c => c.Value);

            var tt = await _dbContext.AspRoleRights.AnyAsync(x => x.AppContent.Area == area && x.AppContent.Controller == controller && x.AppContent.Action == action && loggedInUserRoles.Contains(x.RoleName));


            if (tt) return;
            context.Result = new ForbidResult();
        }


        private bool IsProtectedAction(AuthorizationFilterContext context)
        //it is used to find which controller or action is authorized by authorize attribute.
        //Without authorize attribute ,any controller or action wont be filter by access restriction.
        //So to get a restricted action or controller we must use authorize attribute
        {
            if (context.Filters.Any(item => item is IAllowAnonymousFilter))
                return false;

            var controllerActionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
            var controllerTypeInfo = controllerActionDescriptor.ControllerTypeInfo;
            var actionMethodInfo = controllerActionDescriptor.MethodInfo;

            var authorizeAttribute = controllerTypeInfo.GetCustomAttribute<AuthorizeAttribute>();
            if (authorizeAttribute != null)
                return true;

            authorizeAttribute = actionMethodInfo.GetCustomAttribute<AuthorizeAttribute>();
            if (authorizeAttribute != null)
                return true;

            return false;
        }

        private bool IsUserAuthenticated(AuthorizationFilterContext context)
        {
            return context.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
