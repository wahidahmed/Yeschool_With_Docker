using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace School.AdminService.Data.Entities
{
    public class PersonalInfo:BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PersonalnfoId { get; set; }
        [MaxLength(500)]
        [Required]
        public string PersonName { get; set; }
        [MaxLength(11)]
        public string MobileNo { get; set; }
        [MaxLength(100)]
        public string EmailAddress { get; set; }
        public DateOnly DateOfBirth { get; set; }
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
        public string PersonCode { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<Address> Addresses { get; set; } = new List<Address>();
        public ICollection<StudentInfo> Students { get; set; }= new List<StudentInfo>();
    }
}
