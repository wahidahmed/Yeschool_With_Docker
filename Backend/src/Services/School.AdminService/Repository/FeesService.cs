using Microsoft.EntityFrameworkCore;
using School.AdminService.Data;
using School.AdminService.DataTransferObjects;
using School.AdminService.Repository.Interfaces;

namespace School.AdminService.Repository
{
    public class FeesService : IFeesService
    {
        private readonly AppDbContext context;

        public FeesService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<GetFeesDetailsDto>> GetFeesDetails(long studentId = 0, long feesId = 0, int fromMonth = 0, int toMonth = 0)
        {
            var query = context.FeesCollectionMasters
                        .Include(x => x.StudentInfo)
                        .Include(x => x.FeesCollectionDetails)
                            .ThenInclude(a => a.FeesName).AsQueryable();

            if (feesId > 0)
                query = query.Where(x => x.FeesCollectionDetails.Any(a => a.FeesNameId == feesId));
            
            if (fromMonth > 0 && toMonth > 0)
                query = query.Where(x => x.FeesCollectionDetails.Any(a => a.FromMonth>=fromMonth && a.ToMonth<=toMonth));
            

            if (studentId > 0)
                query = query.Where(x => x.StudentInfoId == studentId);

            var result = await query.Select(s => new GetFeesDetailsDto
            {
                FeesCollectionMasterId = s.FeesCollectionMasterId,
                InvoiceNo = s.InvoiceNo,
                StudentInfoId = s.StudentInfoId,
                AcademicYearId = s.AcademicYearId,
                TotalDiscount = s.TotalDiscount,
                ExtraDiscount = s.ExtraDiscount,
                ExtraDiscountReason = s.ExtraDiscountReason,
                GrandTotalAmount = s.GrandTotalAmount,
                IsAdmitFees = s.IsAdmitFees,
                IsLocked = s.IsLocked,
                CollectDate = s.CollectDate,
                FeesCollectionDetails = s.FeesCollectionDetails
            .Where(d => feesId <= 0 || d.FeesNameId == feesId) // Still filter children
            .Select(d => new GetFeesCollectionDto
            {
                FeesCollectionDetailId = d.FeesCollectionDetailId,
                FeesNameId = d.FeesNameId,
                FromMonth = d.FromMonth,
                ToMonth = d.ToMonth,
                IsSpecial = d.IsSpecial,
                FeesAmount = d.FeesAmount,
                Discount = d.Discount,
                TotalAmount = d.TotalAmount,
                Remarks = d.Remarks,
                FeesName = d.FeesName.Name
            }).ToList()
            }).ToListAsync();

            return result;

        }
    }
}
