using System.ComponentModel.DataAnnotations;

namespace School.AdminService.DataTransferObjects
{
    public class PersonalInfoDto
    {
        public int PersonalnfoId { get; set; }
        [Required]
        public string Name { get; set; }
        [MaxLength(11)]
        public string Mobile { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        public string DateOfBirth { get; set; }
        [Required]
        [MaxLength(100)]
        public string FatherName { get; set; }
        [MaxLength(11)]
        public string FatherMobile { get; set; }
        [MaxLength(100)]
        public string FatherOccupation { get; set; }
        [Required]
        [MaxLength(100)]
        public string MotherName { get; set; }
        [MaxLength(11)]
        public string MotherMobile { get; set; }
        [MaxLength(100)]
        public string MotherOccupation { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [MaxLength(50)]
        public string Religion { get; set; }
        public string ImageUrl { get; set; }
        public string PersonnelCode { get; set; }
    }
}
