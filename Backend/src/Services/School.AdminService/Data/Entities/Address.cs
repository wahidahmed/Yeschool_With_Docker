using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace School.AdminService.Data.Entities
{
    public class Address:BaseEntity
    {
        [Key]
        public Int64 AddressId { get; set; }
        [ForeignKey("District")]
        public int DistrictsId { get; set; }
        [ForeignKey("Thana")]
        public int ThanasId { get; set; }
        [MaxLength(500)]
        public string StreetDetails { get; set; }
        [MaxLength(20)]
        [Required]
        public string AddressType { get; set; }
        [ForeignKey("PersonalInfo")]
        public Int64 PersonalInfoId { get; set; }
        public PersonalInfo PersonalInfo { get; set; }
        public District District { get; set; }
        public Thana Thana { get; set; }
    }
}
