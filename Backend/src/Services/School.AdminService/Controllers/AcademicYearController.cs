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
    public class AcademicYearController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AcademicYearController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await unitOfWork.AcademicYear.GetAsync();
            return Ok(data);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var data = await unitOfWork.AcademicYear.GetByIDAsync(id);
            return Ok(data);
        }


        // api/AcademicYear/GetByActive/true
        [HttpGet("GetByActive/{type?}")]
        public async Task<IActionResult> GetByActive(bool type = true)
        {
            var data = await unitOfWork.AcademicYear.GetFirstOrDefaultAsync(x => x.IsActive == type);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AcademicYearDto dto)
        {
            var entity = mapper.Map<AcademicYear>(dto);
            entity.UpdatedBy = 0;

            unitOfWork.AcademicYear.Insert(entity);
            await unitOfWork.SaveAsync();

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, AcademicYearDto dto)
        {
            if (id != dto.AcademicYearId)
                return BadRequest("Update not allowed");

            var data = await unitOfWork.AcademicYear.GetFirstOrDefaultAsync(x => x.AcademicYearId == id);
            if (data == null)
                return BadRequest("Update not allowed");

            data.AcademicYearId = dto.AcademicYearId;
            data.UpdatedBy = 0;
            unitOfWork.AcademicYear.Update(data);
            await unitOfWork.SaveAsync();
            return StatusCode(201);
        }

        // DELETE api/<AcademicYear>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
