using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.AdminService.Data.Entities;
using School.AdminService.DataTransferObjects;
using School.AdminService.Repository;
using School.AdminService.Repository.Interfaces;

namespace School.AdminService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IIdGeneratorService idGeneratorService;

        public ClassesController(IUnitOfWork unitOfWork, IMapper mapper, IIdGeneratorService idGeneratorService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.idGeneratorService = idGeneratorService;
        }

        // GET: api/<ClassesController>
        [HttpGet]
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> Post(ClassDto classDto)
        {
            var clsName = await unitOfWork.Classes.GetFirstOrDefaultAsync(x => x.ClassesName.ToUpper() == classDto.ClassesName.ToUpper());
            if (clsName != null)
            {
                return BadRequest("This class name has been already exist");
            }
            //throw new Exception("someting test");
            Int64 maxId = await idGeneratorService.GetNextIdAsync("Classes");
            //Int64 maxId = await unitOfWork.Classes.GetMaxIDAsync(x => x.ClassesId);
            //if (classDto.ClassesId == 0)
            //    classDto.ClassesId = Convert.ToInt32(maxId) + 1;
            classDto.ClassesId = Convert.ToInt32(maxId);
            var entity = mapper.Map<Classes>(classDto);
            entity.ClassesName = classDto.ClassesName.ToUpper();
            entity.UpdatedBy = 0;
            entity.UpdatedOn = DateTime.Now;

            var sections = await unitOfWork.Section.GetAsync();

            List<ClassSection> classSection = new List<ClassSection>();
            foreach (var item in sections)
            {
                var clsSec = new ClassSection
                {
                    ClassSectionName= entity.ClassesName+"-"+item.SectionName,
                    ClassesId = entity.ClassesId,
                    SectionId = item.SectionId
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
            if (id != classDto.ClassesId)
                return BadRequest("Update not allowed");

            var classData = await unitOfWork.Classes.GetFirstOrDefaultAsync(x => x.ClassesId == id);
            if (classData == null)
                return BadRequest("Update not allowed");

            classData.ClassesName = classDto.ClassesName;
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
