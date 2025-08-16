using Auth.Api.Data;
using Auth.Api.Data.Entties;
using Auth.Api.Data.RawSql;
using Auth.Api.Modal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrationController : ControllerBase
    {
        private readonly AppDbContext _authContext;
        private readonly IRawSql_Helper rawSql_Helper;

        public AdministrationController(AppDbContext context, IRawSql_Helper rawSql_Helper)
        {
            _authContext = context;
            this.rawSql_Helper = rawSql_Helper;
        }
        [Authorize]
        [HttpPost("AddNewRole")]
        public async Task<IActionResult> Add(string roleName)
        {
            var role = await _authContext.Roles.AnyAsync(x => x.RoleName == roleName.ToUpper());
            if (role)
                return BadRequest(new { Message = "The role is already exist" });
            var entity = new Role
            {
                RoleName = roleName.ToUpper(),
            };
            await _authContext.Roles.AddAsync(entity);
            await _authContext.SaveChangesAsync();
            return Ok();
        }

        [Authorize]
        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _authContext.Roles.ToListAsync();
            return Ok(roles);
        }



        [Authorize]
        [HttpGet("GetAppContentByRole")]
        public IActionResult GetAppContentByRole(string roleName)
        {
            var items = rawSql_Helper.GetAppContentByRole(roleName);
            return Ok(items);
        }

        [Authorize]
        [HttpPost("AssignAccess")]
        public async Task<IActionResult> AssignAccess(AssignAccessDto dto)
        {
            List<AspRoleRight> aspRoleRightList = new List<AspRoleRight>();
            foreach (var menuId in dto.MenuIds)
            {
                AspRoleRight aspRoleRight = new AspRoleRight();
                aspRoleRight.AppContentId = menuId;
                aspRoleRight.RoleName = dto.RoleName;
                aspRoleRightList.Add(aspRoleRight);
            }
            var list= _authContext.AspRoleRights.ToList().Where(x=>x.RoleName==dto.RoleName);
            _authContext.AspRoleRights.RemoveRange(list);

            await _authContext.AspRoleRights.AddRangeAsync(aspRoleRightList);
            await _authContext.SaveChangesAsync();
            return Ok();
        }

        [Authorize]
        [HttpGet("GetMenuItem")]
        public IActionResult GetMenuItem(string userName)
        {
            var items = rawSql_Helper.GetMenuItem(userName);
            return Ok(items);
        }
    }
}
