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
        public Int64 StudentInfosId { get; set; }
        [ForeignKey("AcademicYears")]
        public int AcademicYear { get; set; }

       
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime ActiveDate { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime? InactiveDate { get; set; }

        public StudentInfo StudentInfo { get; set; }
        public FeesName FeesName { get; set; }
        public AcademicYear AcademicYears { get; set; }
    }
}
