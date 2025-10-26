using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.AdminService.Data.Entities
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeId { get; set; }
        [Required]
        [StringLength(150)]
        public string EmployeeName {  get; set; }

        [Precision(18, 2)]
        public decimal BasicSalary {  get; set; }
        [Precision(18, 2)]
        public decimal OthersBenifit { get; set; } = 0;
        public DateOnly JoiningDate { get; set; }
        public DateOnly? ConfirmationDate { get; set; }
        [Required]
        [StringLength(30)]
        public string EmployementType { get; set; }// part time, full time, contractual
        [Required]
        [StringLength(30)]
        public string EmploymentRole { get; set; }// Management,teacher,worker,others
        [StringLength(2000)]
        public string EmployeeDescription { get; set; }
    }
}
