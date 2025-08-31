using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace School.AdminService.Data.Entities
{
    public class StudentInfo:BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int64 StudentId { get; set; }
        [ForeignKey("PersonalInfo")]
        public Int64 PersonalInfoId { get; set; }

        [Required]
        [MaxLength(500)]
        public string GuardianName { get; set; }
        [Required]
        [MaxLength(11)]
        public string GuardianMobile { get; set; }
        [Required]
        [MaxLength(21)]
        public string GuardianRelation { get; set; }
        [ForeignKey("AcademicYears")]
        public int AcademicYear { get; set; }
        [Required]
        [MaxLength(10)]
        public string Status { get; set; }
        [ForeignKey("Classes")]
        public int ClassID { get; set; }
        public PersonalInfo PersonalInfo { get; set; }
        public Classes Classes { get; set; }
        public AcademicYear AcademicYears { get; set; }

        public ICollection<StudentAcademicHistory> StudentAcademicHistory { get; set; } = new List<StudentAcademicHistory>();
    }
}
