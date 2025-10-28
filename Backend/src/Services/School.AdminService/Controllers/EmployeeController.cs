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
    public class EmployeeController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IIdGeneratorService idGeneratorService;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper, IIdGeneratorService idGeneratorService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.idGeneratorService = idGeneratorService;
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> Get()
        {
            var data = await unitOfWork.Employee.GetAsync();
            return Ok(data);
        }

        // GET api/<ClassesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var data = await unitOfWork.Employee.GetByIDAsync(id);
            return Ok(data);
        }

        // POST api/<ClassesController>
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> Post(EmployeeCreateDto dto)
        {
            var personalId = await idGeneratorService.GetNextIdAsync("PersonalInfos");
            dto.personalInfo.PersonalnfoId = personalId;
            dto.personalInfo.PersonCode = "PERS-0" + personalId.ToString();
            var personal = mapper.Map<PersonalInfo>(dto.personalInfo);
            unitOfWork.PersonalInfo.Insert(personal);

            dto.presentAddress.PersonalInfoId = dto.personalInfo.PersonalnfoId;
            dto.presentAddress.AddressType = "PRESENT";
            var presentAdr = mapper.Map<Address>(dto.presentAddress);
            unitOfWork.Address.Insert(presentAdr);

            dto.permanentAddress.PersonalInfoId = dto.personalInfo.PersonalnfoId;
            dto.permanentAddress.AddressType = "PERMANENT";
            var permanentAdr = mapper.Map<Address>(dto.permanentAddress);
            unitOfWork.Address.Insert(permanentAdr);

            Int64 maxId = await idGeneratorService.GetNextIdAsync("Employees");
            var entity = mapper.Map<Employee>(dto.employeeDto);
            entity.EmployeeId = Convert.ToInt32(maxId);
            entity.PersonalInfoId = dto.personalInfo.PersonalnfoId;
            unitOfWork.Employee.Insert(entity);
            await unitOfWork.SaveAsync();

            return StatusCode(201);
        }

        // PUT api/<ClassesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long personalId, EmployeeUpdateDto dto)
        {
            var personalData = await unitOfWork.PersonalInfo.GetByIDAsync(personalId);
            if (personalData == null)
                return BadRequest("Update not allowed personal info");

            personalData.UpdatedOn = DateTime.Now; personalData.UpdatedBy = 1;
            mapper.Map(dto.personalInfo, personalData);
            unitOfWork.PersonalInfo.Update(personalData);

            var data = await unitOfWork.Employee.GetFirstOrDefaultAsync(x => x.PersonalInfoId == personalId);
            if (data == null)
                return BadRequest("Update not allowed student info");

            mapper.Map(dto.employeeDto, data);
            unitOfWork.Employee.Update(data);

            var presentData = await unitOfWork.Address.GetFirstOrDefaultAsync(x => x.PersonalInfoId == personalId && x.AddressType == "PRESENT");
            if (presentData == null)
                return BadRequest("Update not allowed for present address");

            presentData.UpdatedOn = DateTime.Now; presentData.UpdatedBy = 1;
            mapper.Map(dto.presentAddress, presentData);
            unitOfWork.Address.Update(presentData);

            var permanentData = await unitOfWork.Address.GetFirstOrDefaultAsync(x => x.PersonalInfoId == personalId && x.AddressType == "PERMANENT");
            if (permanentData == null)
                return BadRequest("Update not allowed for permanent address");

            permanentData.UpdatedOn = DateTime.Now; permanentData.UpdatedBy = 1;
            mapper.Map(dto.permanentAddress, permanentData);
            unitOfWork.Address.Update(permanentData);

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
