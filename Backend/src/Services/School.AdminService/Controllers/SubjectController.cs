using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.AdminService.Data.Entities;
using School.AdminService.DataTransferObjects;
using School.AdminService.Repository.Interfaces;

namespace School.AdminService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IIdGeneratorService idGeneratorService;

        public SubjectController(IUnitOfWork unitOfWork, IMapper mapper, IIdGeneratorService idGeneratorService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.idGeneratorService = idGeneratorService;
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> Get()
        {
            var data = await unitOfWork.Subject.GetAsync();
            return Ok(data);
        }

        // GET api/<ClassesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var data = await unitOfWork.Subject.GetByIDAsync(id);
            return Ok(data);
        }

        // POST api/<ClassesController>
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> Post(SubjectDto dto)
        {
            var subName = await unitOfWork.Subject.GetFirstOrDefaultAsync(x => x.SubjectName.ToUpper() == dto.SubjectName.ToUpper());
            if (subName != null)
            {
                return BadRequest("This subject name has been already exist");
            }
            //throw new Exception("someting test");
            Int64 maxId = await idGeneratorService.GetNextIdAsync("Subjects");
            dto.SubjectId = Convert.ToInt32(maxId);
            var entity = mapper.Map<Subject>(dto);
            entity.SubjectName = dto.SubjectName.ToUpper();

            unitOfWork.Subject.Insert(entity);
            await unitOfWork.SaveAsync();

            return StatusCode(201);
        }

        // PUT api/<ClassesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, SubjectDto dto)
        {
            if (id != dto.SubjectId)
                return BadRequest("Update not allowed");

            var data = await unitOfWork.Subject.GetFirstOrDefaultAsync(x => x.SubjectId == id);
            if (data == null)
                return BadRequest("Update not allowed");

            data.SubjectName = dto.SubjectName;
            data.Remarks = dto.Remarks;
            unitOfWork.Subject.Update(data);
            await unitOfWork.SaveAsync();
            return StatusCode(201);
        }

        // DELETE api/<ClassesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
