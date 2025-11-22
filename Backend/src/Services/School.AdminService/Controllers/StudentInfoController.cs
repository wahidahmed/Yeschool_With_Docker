using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using School.AdminService.Repository.Interfaces;

namespace School.AdminService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentInfoController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IStudentService studentService;
        private readonly IIdGeneratorService idGeneratorService;

        public StudentInfoController(IUnitOfWork unitOfWork, IMapper mapper, IStudentService studentService, IIdGeneratorService idGeneratorService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.studentService = studentService;
            this.idGeneratorService = idGeneratorService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(long personalId = 0, long studentId = 0, int classId = 0)
        {
            var fullData = await studentService.GetStudentInfoAsync(personalId, studentId, classId);
            if (!fullData.Any())
            {
                return NotFound();
            }
            return Ok(fullData);
        }
    }
}
