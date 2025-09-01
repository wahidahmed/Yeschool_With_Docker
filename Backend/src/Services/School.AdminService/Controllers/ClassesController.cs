using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using School.AdminService.Data.Entities;
using School.AdminService.DataTransferObjects;
using School.AdminService.Repository.Interfaces;

namespace School.AdminService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ClassesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        // GET: api/<ClassesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await unitOfWork.Classes.GetAsync();
            return Ok(data);
        }

        // GET api/<ClassesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var data = await unitOfWork.Classes.GetByIDAsync(id);
            return Ok(data);
        }

        // POST api/<ClassesController>
        [HttpPost]
        public async Task<IActionResult> Post(ClassDto classDto)
        {
            //throw new Exception("someting test");
            Int64 maxId = await unitOfWork.Classes.GetMaxIDAsync(x => x.ClassesId);
            if (classDto.ClassID == 0)
                classDto.ClassID = Convert.ToInt32(maxId) + 1;
            var entity = mapper.Map<Classes>(classDto);
            entity.UpdatedBy = 0;

            var sections = await unitOfWork.Section.GetAsync();

            List<ClassSection> classSection = new List<ClassSection>();
            foreach (var item in sections)
            {
                var clsSec = new ClassSection
                {
                    ClassesId = entity.ClassesId,
                    SectionId = item.SectionId,
                    UpdatedBy = 0
                };
                classSection.Add(clsSec);
            }

            unitOfWork.Classes.Insert(entity);
            unitOfWork.ClassSection.InsertRange(classSection);
            await unitOfWork.SaveAsync();

            return StatusCode(201);
        }

        // PUT api/<ClassesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ClassDto classDto)
        {
            if (id != classDto.ClassID)
                return BadRequest("Update not allowed");

            var classData = await unitOfWork.Classes.GetFirstOrDefaultAsync(x => x.ClassesId == id);
            if (classData == null)
                return BadRequest("Update not allowed");

            classData.ClassesName = classDto.ClassName;
            classData.Remarks = classDto.Remarks;
            classData.UpdatedBy = 0;
            unitOfWork.Classes.Update(classData);
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
