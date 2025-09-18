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
        public long StudentInfoId { get; set; }
        [Required]
        public int AcademicYearId { get; set; }
        public decimal TotalParialDiscount { get; set; }

        public decimal ExtraDiscount { get; set; }

        public string ExtraDiscountReason { get; set; }

        public decimal GrandTotalAmount { get; set; }

        public bool IsAdmitFees { get; set; }

        public bool IsLocked { get; set; }
        public List<FeesDetailsDto> FeesCollectionDetails { get; set; }
    }

    public class FeesDetailsDto
    {
        public long FeesCollectionDetailId { get; set; }

        public long FeesCollectionMasterId { get; set; }
        public int FeesNameId { get; set; }

        public int? FromMonth { get; set; }

        public int? ToMonth { get; set; }

        public bool IsSpecial { get; set; } = false;
        public decimal FeesAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        [MaxLength(500)]
        public string Remarks { get; set; }
    }

}
