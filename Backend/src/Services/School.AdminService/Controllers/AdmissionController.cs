using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.AdminService.Data.Entities;
using School.AdminService.DataTransferObjects;
using School.AdminService.Helpers;
using School.AdminService.Repository.Interfaces;
using System.Threading.Tasks;

namespace School.AdminService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdmissionController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AdmissionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            return Ok();
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(AdmissionDto dto)
        {
            var personalId =Convert.ToInt64(unitOfWork.PersonalInfo.GetMaxID(x=>x.PersonalnfoId))+1;
            dto.personalInfo.PersonalnfoId = personalId;
            dto.personalInfo.PersonCode = "PERS-0"+personalId.ToString();
            var personal=mapper.Map<PersonalInfo>(dto.personalInfo);
            unitOfWork.PersonalInfo.Insert(personal);

            dto.presentAddress.PersonalInfoId= dto.personalInfo.PersonalnfoId;
            dto.presentAddress.AddressType = "presnt";
            var presentAdr=mapper.Map<Address>(dto.presentAddress);
            unitOfWork.Address.Insert(presentAdr);

            dto.permanentAddress.PersonalInfoId = dto.personalInfo.PersonalnfoId;
            dto.permanentAddress.AddressType = "permanent";
            var permanentAdr = mapper.Map<Address>(dto.permanentAddress);
            unitOfWork.Address.Insert(permanentAdr);

            var stuId = Convert.ToInt64(unitOfWork.StudentInfo.GetMaxID(x => x.StudentId)) + 1;
            dto.studentInfo.StudentId = stuId;
            dto.studentInfo.PersonalInfoId = dto.personalInfo.PersonalnfoId;
            var studentInfo = mapper.Map<StudentInfo>(dto.studentInfo);
            unitOfWork.StudentInfo.Insert(studentInfo);

            await unitOfWork.SaveAsync();

            return Ok(200);
        }

        [HttpPut]
        public async Task<IActionResult> Put(long personalId,AdmissionUpdateDto dto)
        {
           
            var personalData = await unitOfWork.PersonalInfo.GetByIDAsync(personalId);
            if (personalData == null)
                return BadRequest("Update not allowed personal info");

            personalData.UpdatedOn = DateTime.Now;personalData.UpdatedBy = 1;
            mapper.Map(dto.personalInfo, personalData);
            unitOfWork.PersonalInfo.Update(personalData);
            
            var studentData=await unitOfWork.StudentInfo.GetFirstOrDefaultAsync(x=>x.PersonalInfoId==personalId);
            if (studentData == null)
                return BadRequest("Update not allowed student info");

            studentData.UpdatedOn = DateTime.Now; studentData.UpdatedBy = 1;
            mapper.Map(dto.studentInfo, studentData);
            unitOfWork.StudentInfo.Update(studentData);

            var presentData = await unitOfWork.Address.GetFirstOrDefaultAsync(x => x.PersonalInfoId == personalId && x.AddressType=="presnt");
            if (presentData == null)
                return BadRequest("Update not allowed for present address");

            presentData.UpdatedOn = DateTime.Now; presentData.UpdatedBy = 1;
            mapper.Map(dto.presentAddress, presentData);
            unitOfWork.Address.Update(presentData);

            var permanentData = await unitOfWork.Address.GetFirstOrDefaultAsync(x => x.PersonalInfoId == personalId && x.AddressType=="permanent");
            if (permanentData == null)
                return BadRequest("Update not allowed for permanent address");

            permanentData.UpdatedOn = DateTime.Now; studentData.UpdatedBy = 1;
            mapper.Map(dto.permanentAddress, permanentData);
            unitOfWork.Address.Update(permanentData);

            await unitOfWork.SaveAsync();

            return Ok(200);
        }
    }
}
