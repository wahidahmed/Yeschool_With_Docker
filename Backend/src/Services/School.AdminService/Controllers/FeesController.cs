using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using School.AdminService.Data.Entities;
using School.AdminService.DataTransferObjects;
using School.AdminService.Helpers;
using School.AdminService.Repository.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace School.AdminService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IFeesService feesService;

        public FeesController(IUnitOfWork unitOfWork, IMapper mapper, IFeesService feesService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.feesService = feesService;
        }
        [HttpPost("AddFeesName")]
        public async Task<IActionResult> AddFeesName(FeesNameDto dto)
        {
            var entity = mapper.Map<FeesName>(dto);
            unitOfWork.FeesName.Insert(entity);
            await unitOfWork.SaveAsync();
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await unitOfWork.FeesName.GetAsync();
            return Ok(data);
        }
        [HttpPut("UpdateFeesName")]
        public async Task<IActionResult> UpdateFeesName(int id, FeesNameUpdateDto dto)
        {
            var data = await unitOfWork.FeesName.GetByIDAsync(id);
            if (data == null)
            {
                return NotFound("There is no data by this Id");
            }
            data.UpdatedOn = DateTime.Now;
            data.Name = dto.Name;
            var entity = mapper.Map(dto, data);
            unitOfWork.FeesName.Update(entity);
            var result = await unitOfWork.SaveAsync();
            return Ok(result);
        }

        [HttpPost("AdmitEnrolled")]
        public async Task<IActionResult> Enrolled(FeesCollectionDto dto)
        {
            var studentInfo = await unitOfWork.StudentInfo.GetByIDAsync(dto.StudentInfoId);
            if (studentInfo == null)
            {
                return NotFound("There is no data by this Id");
            }

            var masterEntity = await FeesCollectionMap(dto);
            masterEntity.IsAdmitFees = true;
            unitOfWork.FeesCollectionMaster.Insert(masterEntity);
            studentInfo.UpdatedOn = DateTime.Now;
            studentInfo.UpdatedBy = 1;
            studentInfo.Status = "ENROLLED";
            unitOfWork.StudentInfo.Update(studentInfo);
            var retult = await unitOfWork.SaveAsync();
            return Ok(retult);
        }

        [HttpPost("FeesCollection")]
        public async Task<IActionResult> FeesCollection(FeesCollectionDto dto)
        {
            var studentInfo = await unitOfWork.StudentInfo.GetFirstOrDefaultAsync(x => x.StudentId == dto.StudentInfoId && x.Status != "PENDING");
            if (studentInfo == null)
            {
                return NotFound("There is no data by this Id");
            }
            var masterEntity = await FeesCollectionMap(dto);

            unitOfWork.FeesCollectionMaster.Insert(masterEntity);
            var retult = await unitOfWork.SaveAsync();
            return Ok(retult);
        }

        [HttpGet("GetFeesDetails")]
        public async Task<IActionResult> GetFeesDetails(long studentId = 0,int feesId=0,int fromMonth=0, int toMonth=0)
        {
            var result = await feesService.GetFeesDetails(studentId, feesId, fromMonth, toMonth);
            if (!result.Any())
            {
                return NotFound("not data found");
            }
            return Ok(result);
        }

        private async Task<FeesCollectionMaster> FeesCollectionMap(FeesCollectionDto dto)
        {
            string invoiceNo;
            do
            {
                invoiceNo = InvoiceGenerator.GenerateInvoiceNo();
            }
            while (await unitOfWork.FeesCollectionMaster.AnyAsync(x => x.InvoiceNo == invoiceNo));

            var masterEntity = mapper.Map<FeesCollectionMaster>(dto);
            masterEntity.InvoiceNo = invoiceNo;
            masterEntity.CreatedBy = 1;
            masterEntity.CreatedOn = DateTime.Now;
            return masterEntity;
        }

    }
}
