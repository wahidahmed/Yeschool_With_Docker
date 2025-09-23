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
    public class SectionController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public SectionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        // GET: api/<SectionController>
        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> Get()
        {
            var data = await unitOfWork.Section.GetAsync();
            return Ok(data);
        }

        // GET api/<SectionController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var data = await unitOfWork.Section.GetByIDAsync(id);
            return Ok(data);
        }

        // POST api/<SectionController>
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> Post(SectionDto dto)
        {
            Int64 maxId = await unitOfWork.Section.GetMaxIDAsync(x => x.SectionId);

            if (dto.SectionId == 0)
                dto.SectionId = Convert.ToInt32(maxId) + 1;
            var entity = mapper.Map<Section>(dto);
            entity.SectionName = dto.SectionName.ToUpper();
            entity.CreatedBy = 0;
            entity.CreatedOn = DateTime.Now;

            var classes = await unitOfWork.Classes.GetAsync();

            List<ClassSection> classSection = new List<ClassSection>();
            foreach (var item in classes)
            {
                var clsSec = new ClassSection
                {
                    ClassSectionName = item.ClassesName + "-" + entity.SectionName,
                    SectionId = entity.SectionId,
                    ClassesId = item.ClassesId
                };
                classSection.Add(clsSec);
            }

            unitOfWork.Section.Insert(entity);
            unitOfWork.ClassSection.InsertRange(classSection);
            await unitOfWork.SaveAsync();

            return StatusCode(201);
        }

        // PUT api/<SectionController>/5
        [HttpPut("{id}")]
        //[Authorize]
        public async Task<IActionResult> Put(int id, SectionDto sectionDto)
        {
            if (id != sectionDto.SectionId)
                return BadRequest("Update not allowed");

            var secData = await unitOfWork.Section.GetFirstOrDefaultAsync(x => x.SectionId == id);
            if (secData == null)
                return BadRequest("Update not allowed");

            secData.SectionName = sectionDto.SectionName;
            secData.Remarks = sectionDto.Remarks;
            secData.UpdatedBy = 0;
            unitOfWork.Section.Update(secData);
            await unitOfWork.SaveAsync();
            return StatusCode(201);
        }

        // DELETE api/<SectionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // GET: api/<SectionController>/GetClassSection
        [HttpGet("GetClassSection")]
        public async Task<IActionResult> GetClassSection()
        {
            var data = await unitOfWork.ClassSection.GetAsync(null, null, "Classes,Section");
            return Ok(data);
        }

        // GET: api/<SectionController>/GetClassSectionByClassId/1
        [HttpGet("GetClassSectionByClassId/{classId}")]
        public async Task<IActionResult> GetClassSectionByClassId(int classId)
        {
            var data = await unitOfWork.ClassSection.GetAsync(null, null, "Classes,Section");
            return Ok(data.Where(x => x.ClassesId == classId).ToList());
        }
    }
}
