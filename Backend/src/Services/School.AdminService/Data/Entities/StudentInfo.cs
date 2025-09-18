using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using School.AdminService.Helpers;

namespace School.AdminService.Data.Entities
{
    public class StudentInfo:BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long StudentId { get; set; }
        [ForeignKey("PersonalInfo")]
        public long PersonalInfoId { get; set; }

        [Required]
        [MaxLength(500)]
        public string GuardianName { get; set; }
        [Required]
        [MaxLength(11)]
        public string GuardianMobileNo { get; set; }
        [Required]
        [MaxLength(21)]
        public string GuardianRelation { get; set; }
        [Required]
        [MaxLength(10)]
        public string Status { get; set; }

        [ForeignKey("Classes")]
        public int ClassesId {  get; set; }
        [ForeignKey("AcademicYear")]
        public int AcademicYearId { get; set; }
        public Classes Classes { get; set; }
        public PersonalInfo PersonalInfo { get; set; }
        public AcademicYear AcademicYear { get; set; }
        public ICollection<StudentAcademicHistory> studentAcademicHistories { get; set; }=new List<StudentAcademicHistory>();

    }

    
}
