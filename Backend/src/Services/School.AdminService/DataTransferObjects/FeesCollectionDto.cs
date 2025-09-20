using System.ComponentModel.DataAnnotations;

namespace School.AdminService.DataTransferObjects
{
    public class FeesCollectionDto
    {
       
        [Required]
        public long StudentInfoId { get; set; }
        [Required]
        public int AcademicYearId { get; set; }
        public decimal TotalParialDiscount { get; set; }

        public decimal ExtraDiscount { get; set; }

        public string ExtraDiscountReason { get; set; }

        public decimal GrandTotalAmount { get; set; }
        public string CollectDate { get; set; }

        public List<FeesDetailsDto> FeesCollectionDetails { get; set; }
    }

    public class FeesCollectionUpdateDto
    {

        [Required]
        public int AcademicYearId { get; set; }
        public decimal TotalParialDiscount { get; set; }

        public decimal ExtraDiscount { get; set; }

        public string ExtraDiscountReason { get; set; }

        public decimal GrandTotalAmount { get; set; }
        public string CollectDate { get; set; }

        public List<GetFeesDetailsDto> FeesCollectionDetails { get; set; }
    }

    public class FeesDetailsDto
    {
        public int FeesNameId { get; set; }

        public int? FromMonth { get; set; }

        public int? ToMonth { get; set; }
        public decimal FeesAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        [MaxLength(500)]
        public string Remarks { get; set; }
    }

}
