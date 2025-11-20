using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using School.AdminService.Data.Entities;
using School.AdminService.DataTransferObjects;
using School.AdminService.Repository.Interfaces;

namespace School.AdminService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController(IUnitOfWork unitOfWork, IMapper mapper, IIdGeneratorService idGeneratorService) : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;
        private readonly IMapper mapper = mapper;
        private readonly IIdGeneratorService idGeneratorService = idGeneratorService;

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> Get()
        {
            var data = await unitOfWork.ScheduleMaster.GetAsync(null,null, "ScheduleDetails");
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var data = await unitOfWork.ScheduleMaster.GetFirstOrDefaultAsync(x=>x.ScheduleMasterId==id,null, "ScheduleDetails");
            return Ok(data);
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> Post(ScheduleDto dto)
        {
            var id = await idGeneratorService.GetNextIdAsync("ScheduleMasters");
            
            var entity = mapper.Map<ScheduleMaster>(dto);
            entity.ScheduleDetails = null;
            entity.ScheduleMasterId =Convert.ToInt32(id);
            entity.CreatedBy = 1;
            entity.CreatedOn = DateTime.UtcNow;
            unitOfWork.ScheduleMaster.Insert(entity);

            var detail=mapper.Map<List<ScheduleDetail>>(dto.scheduleDetailDtos);
            foreach(var item in detail)
            {
                item.ScheduleMasterId = Convert.ToInt32(entity.ScheduleMasterId);
                id = await idGeneratorService.GetNextIdAsync("ScheduleDetail");
                item.ScheduleDetailId = Convert.ToInt32(id);
            }
            
            unitOfWork.ScheduleDetail.InsertRange(detail);
            await unitOfWork.SaveAsync();
            return StatusCode(201);
        }

        [HttpPut]
        //[Authorize]
        public async Task<IActionResult> Put(ScheduleUpdateDto dto)
        {

            unitOfWork.ScheduleDetail.DeleteWhere(x=>x.ScheduleMasterId == dto.ScheduleMasterId);
            var entity=await unitOfWork.ScheduleMaster.GetByIDAsync(dto.ScheduleMasterId);
            if(entity == null)
                return NotFound("not found any data");

            entity.TotalPeriod = dto.TotalPeriod;
            entity.ThePeriod_Before_TiffinBreak=dto.ThePeriod_Before_TiffinBreak;
            entity.BreakMinutes = dto.BreakMinutes;
            entity.ClassSectionId = dto.ClassSectionId;
            entity.DurationInMinutes = dto.DurationInMinutes;
            entity.ScheduleStartTime = dto.ScheduleStartTime;
            entity.UpdatedBy = 1;
            entity.UpdatedOn=DateTime.UtcNow;
            entity.ScheduleDetails = null;
            unitOfWork.ScheduleMaster.Update(entity);

            var detail = mapper.Map<List<ScheduleDetail>>(dto.scheduleDetailDtos);
            foreach (var item in detail)
            {
                item.ScheduleMasterId = Convert.ToInt32(entity.ScheduleMasterId);
                var id = await idGeneratorService.GetNextIdAsync("ScheduleDetail");
                item.ScheduleDetailId = Convert.ToInt32(id);
            }

            unitOfWork.ScheduleDetail.InsertRange(detail);
            await unitOfWork.SaveAsync();
            return StatusCode(201);
        }
    }
}
