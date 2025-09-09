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
        
        [HttpPost]
        public async Task<IActionResult> Post(AdmissionDto dto)
        {
            dto.personalInfo.PersonalnfoId = StaticHelpers.GeneratId();
            var personal=mapper.Map<PersonalInfo>(dto.personalInfo);
            unitOfWork.PersonalInfo.Insert(personal);

            dto.presentAddress.AddressId=StaticHelpers.GeneratId();
            dto.presentAddress.PersonalInfoId= dto.personalInfo.PersonalnfoId;
            var presentAdr=mapper.Map<Address>(dto.presentAddress);
            unitOfWork.Address.Insert(presentAdr);

            dto.permanentAddress.AddressId=StaticHelpers.GeneratId();
            dto.permanentAddress.PersonalInfoId = dto.personalInfo.PersonalnfoId;
            var permanentAdr = mapper.Map<Address>(dto.permanentAddress);
            unitOfWork.Address.Insert(permanentAdr);

            dto.studentInfo.StudentId = StaticHelpers.GeneratId();
            dto.studentInfo.PersonalInfoId = dto.personalInfo.PersonalnfoId;
            var studentInfo=mapper.Map<StudentInfo>(dto.studentInfo);
            unitOfWork.StudentInfo.Insert(studentInfo);

            await unitOfWork.SaveAsync();

            return Ok();
        }
    }
}
