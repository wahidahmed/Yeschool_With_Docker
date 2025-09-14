using System.ComponentModel.DataAnnotations;

namespace School.AdminService.DataTransferObjects
{
    public class AdmissionUpdateDto
    {
        public StudentInfoUpdateDto studentInfo { get; set; }
        public PersonalInfoUpdateDto personalInfo { get; set; }
        public AddressUpdateDto presentAddress { get; set; }
        public AddressUpdateDto permanentAddress { get; set; }
    }
    public class StudentInfoUpdateDto
    {
        public long StudentId { get; set; }
        public long? PersonalInfoId { get; set; }

        [RegularExpression(".*[a-zA-Z]+.*", ErrorMessage = "Guardian Name cannot contain numeric value")]
        [MaxLength(500)]
        public string GuardianName { get; set; }

        [MaxLength(11, ErrorMessage = "Must contain 11 digit")]
        public string GuardianMobileNo { get; set; }

        [MaxLength(21, ErrorMessage = "cannot contain more than 21 digit")]
        public string GuardianRelation { get; set; }


        [MaxLength(10)]
        public string Status { get; set; }

        public int? ClassID { get; set; }
    }
    public class PersonalInfoUpdateDto
    {
        public long PersonalnfoId { get; set; }
        public string PersonName { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string DateOfBirth { get; set; }
        [MaxLength(100)]
        public string FatherName { get; set; }
        [MaxLength(11)]
        public string FatherMobile { get; set; }
        [MaxLength(100)]
        public string FatherOccupation { get; set; }
        [MaxLength(100)]
        public string MotherName { get; set; }
        [MaxLength(11)]
        public string MotherMobile { get; set; }
        [MaxLength(100)]
        public string MotherOccupation { get; set; }
        public string Gender { get; set; }
        [MaxLength(50)]
        public string Religion { get; set; }
        public string ImageUrl { get; set; }
        public string PersonCode { get; set; }
    }
    public class AddressUpdateDto
    {
        public long AddressId { get; set; }
        public int? DistrictId { get; set; }
        public int? ThanaId { get; set; }
        public string StreetDetail { get; set; }
        public string AddressType { get; set; }
        public string ThanaName { get; set; }
        public string DistrictName { get; set; }
        public long? PersonalInfoId { get; set; }
    }
}
