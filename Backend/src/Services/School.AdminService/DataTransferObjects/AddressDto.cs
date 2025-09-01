using System.ComponentModel.DataAnnotations;

namespace School.AdminService.DataTransferObjects
{
    public class AddressDto
    {
        public int AddressId { get; set; }
        public int DistrictsId { get; set; }
        public int ThanasId { get; set; }
        public string StreetDetails { get; set; }
        [Required]
        public string AddressType { get; set; }
        public int PersonalInfoId { get; set; }
    }
}
