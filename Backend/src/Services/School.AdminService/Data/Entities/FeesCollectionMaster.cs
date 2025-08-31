using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.AdminService.Data.Entities
{
    public class FeesCollectionMaster:BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long FeesCollectionMasterId { get; set; }

        [Required]
        [MaxLength(20)]
        public string InvoiceNo { get; set; }

        [ForeignKey("StudentInfo")]
        public Int64 StudentInfoId { get; set; }

        [ForeignKey("AcademicYear")]
        public int AcademicYearId { get; set; }

        public decimal TotalParialDiscount { get; set; }

        public decimal ExtraDiscount { get; set; }

        public string ExtraDiscountReason { get; set; }

        public decimal GrandTotalAmount { get; set; }

        public bool IsAdmitFees { get; set; }

        public bool IsLocked { get; set; }

        public DateTime CollectDate { get; set; }

        public AcademicYear AcademicYear { get; set; }

        public StudentInfo StudentInfo { get; set; }
    }
}
