using School.AdminService.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace School.AdminService.DataTransferObjects
{
    public class FeesCollectionDto
    {
        public long FeesCollectionMasterId { get; set; }
        [Required]
        [MaxLength(20)]
        public string InvoiceNo { get; set; }
        [Required]
        public Int64 StudentInfoId { get; set; }
        [Required]
        public int AcademicYearId { get; set; }
        public decimal TotalParialDiscount { get; set; }

        public decimal ExtraDiscount { get; set; }

        public string ExtraDiscountReason { get; set; }

        public decimal GrandTotalAmount { get; set; }

        public bool IsAdmitFees { get; set; }

        public bool IsLocked { get; set; }
        public List<FeesCollectionDetail> FeesListArray { get; set; }
    }
}
