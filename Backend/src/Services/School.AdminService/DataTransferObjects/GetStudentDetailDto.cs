using System.ComponentModel.DataAnnotations;

namespace School.AdminService.DataTransferObjects
{
    public class GetStudentDetailDto
    {
        public long StudentId { get; set; }
        public long PersonalInfoId { get; set; }
        public string GuardianName { get; set; }
        public string GuardianMobileNo { get; set; }
        public string GuardianRelation { get; set; }
        public string Status { get; set; }
        public string ClassesName { get; set; }
        public string SectionName { get; set; }
        public int ClassesId { get; set; }
        public int SectionId { get; set; }
        public int AcademicYearId {  get; set; }
        public bool IsActive { get; set; }



        public string PersonName { get; set; }
        public string MobileNo { get; set; }
        public string EmailAddress { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string FatherName { get; set; }
        public string FatherMobile { get; set; }
        public string FatherOccupation { get; set; }
        public string MotherName { get; set; }
        public string MotherMobile { get; set; }
        public string MotherOccupation { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public string PersonCode { get; set; }
        public string ImageUrl { get; set; }

        public AddressUpdateDto PresentAddress { get; set; }

        public AddressUpdateDto PermanentAddress { get; set; }
    }


}
