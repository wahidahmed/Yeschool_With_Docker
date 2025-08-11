using Auth.Api.Data;
using Auth.Api.Data.Entties;
using Auth.Api.Data.RawSql;
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
        [HttpGet("GetMenuItem")]
        public IActionResult GetMenuItem(string roleName)
        {
            var items = rawSql_Helper.GetAppContentByRole(roleName);
            return Ok(items);
        }
    }
}
