using System.ComponentModel.DataAnnotations;

namespace School.AdminService.DataTransferObjects
{
    public class AddressDto
    {
        public long AddressId { get; set; }
        public int DistrictId { get; set; }
        public int ThanaId { get; set; }
        public string StreetDetail { get; set; }
        public string AddressType { get; set; }
        public long? PersonalInfoId { get; set; }
    }
}
