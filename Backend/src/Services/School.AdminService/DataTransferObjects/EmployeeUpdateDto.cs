using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace School.AdminService.DataTransferObjects
{
    public class EmployeeUpdateDto
    {
        public EmployeeDto employeeDto { get; set; }
        public PersonalInfoUpdateDto personalInfo { get; set; }
        public AddressUpdateDto presentAddress { get; set; }
        public AddressUpdateDto permanentAddress { get; set; }
    }
}
