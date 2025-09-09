using System.ComponentModel.DataAnnotations;

namespace School.AdminService.DataTransferObjects
{
    public class StudentInfoDto
    {
        public long StudentId { get; set; }
        public long PersonalInfoId { get; set; }

        [Required(ErrorMessage = "Guardian Name is required")]
        [RegularExpression(".*[a-zA-Z]+.*", ErrorMessage = "Guardian Name cannot contain numeric value")]
        [MaxLength(500)]
        public string GuardianName { get; set; }

        [Required(ErrorMessage = "Guardian mobile number is required")]
        [MaxLength(11,ErrorMessage ="Must contain 11 digit")]
        public string GuardianMobile { get; set; }

        [Required(ErrorMessage = "Guardian relation is required")]
        [MaxLength(21, ErrorMessage = "cannot contain more than 21 digit")]
        public string GuardianRelation { get; set; }

        public int AcademicYear { get; set; }

        [Required]
        [MaxLength(10)]
        public string Status { get; set; }

        public int ClassID { get; set; }
    }
}
