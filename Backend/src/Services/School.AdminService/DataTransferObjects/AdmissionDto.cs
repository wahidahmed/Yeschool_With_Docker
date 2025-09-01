namespace School.AdminService.DataTransferObjects
{
    public class AdmissionDto
    {
      
        public StudentInfoDto studentInfo { get; set; }
        public PersonalInfoDto personalInfo { get; set; }
        public AddressDto presentAddress { get; set; }
        public AddressDto permanentAddress { get; set; }
    }
}
