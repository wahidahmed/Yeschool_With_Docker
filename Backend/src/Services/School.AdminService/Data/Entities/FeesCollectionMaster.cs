using Microsoft.EntityFrameworkCore;
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
        [MaxLength(30)]
        public string InvoiceNo { get; set; }

        [ForeignKey("StudentInfo")]
        public long StudentInfoId { get; set; }

        [ForeignKey("AcademicYear")]
        public int AcademicYearId { get; set; }
        [Precision(18, 2)]
        public decimal TotalDiscount { get; set; } = 0;
        [Precision(18, 2)]
        public decimal ExtraDiscount { get; set; } = 0;
        [MaxLength(500)]
        public string ExtraDiscountReason { get; set; }
        [Precision(18, 2)]
        public decimal GrandTotalAmount { get; set; }

        public bool IsAdmitFees { get; set; }=false;

        public bool IsLocked { get; set; } = false;

        public DateOnly CollectDate { get; set; }

        public AcademicYear AcademicYear { get; set; }

        public StudentInfo StudentInfo { get; set; }

        public ICollection<FeesCollectionDetail> FeesCollectionDetails { get; set; }=new List<FeesCollectionDetail>();
    }
}
