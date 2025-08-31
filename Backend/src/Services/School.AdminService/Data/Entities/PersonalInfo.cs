using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace School.AdminService.Data.Entities
{
    public class PersonalInfo:BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int64 PersonalnfoId { get; set; }
        [MaxLength(500)]
        [Required]
        public string Name { get; set; }
        [MaxLength(11)]
        public string Mobile { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
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
        public string PersonnelCode { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<Address> Address { get; set; } = new List<Address>();
    }
}
