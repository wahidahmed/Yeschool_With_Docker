using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace School.AdminService.Data.Entities
{
    public class Address:BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AddressId { get; set; }
        [ForeignKey("District")]
        public int DistrictId { get; set; }
        [ForeignKey("Thana")]
        public int ThanaId { get; set; }
        [MaxLength(500)]
        public string StreetDetail { get; set; }
        [MaxLength(20)]
        [Required]
        public string AddressType { get; set; }
        [ForeignKey("PersonalInfo")]
        public long PersonalInfoId { get; set; }
        public PersonalInfo PersonalInfo { get; set; }
        public District District { get; set; }
        public Thana Thana { get; set; }
    }
}
