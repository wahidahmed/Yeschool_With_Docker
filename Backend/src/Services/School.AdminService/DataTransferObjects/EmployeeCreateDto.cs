using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace School.AdminService.DataTransferObjects
{
    public class EmployeeCreateDto
    {
     
       
        public EmployeeDto employeeDto { get; set; }
        public PersonalInfoDto personalInfo { get; set; }
        public AddressDto presentAddress { get; set; }
        public AddressDto permanentAddress { get; set; }
    }

}
