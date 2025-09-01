using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.AdminService.Data.Entities
{
    public class SpecialFee:BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SpecialFeesId { get; set; }
        [ForeignKey("FeesName")]
        public int FeesNamesId { get; set; }
        [ForeignKey("StudentInfo")]
        public long StudentInfosId { get; set; }
        [ForeignKey("AcademicYears")]
        public int AcademicYear { get; set; }

        [Precision(18,2)]
        public decimal Amount { get; set; }
        [MaxLength(500)]
        public string Remarks { get; set; }
        public bool IsActive { get; set; }=false;

        public DateOnly ActiveDate { get; set; }
        public DateOnly? InactiveDate { get; set; }

        public StudentInfo StudentInfo { get; set; }
        public FeesName FeesName { get; set; }
        public AcademicYear AcademicYears { get; set; }
    }
}
