using Microsoft.AspNetCore.Mvc;
using School.AdminService.Models;
using School.AdminService.Repository.Interfaces;

namespace School.AdminService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentInfoController : ControllerBase
    {
        private readonly IStudentService studentService;

        public StudentInfoController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] StudentParams studentParams)
        {
            var fullData = await studentService.GetStudentInfoAsync(studentParams.personalId, studentParams.studentId, studentParams.classId, studentParams.page, studentParams.pageSize);
            if (!fullData.Any())
            {
                return NotFound();
            }
            return Ok(fullData);
        }
    }
}
