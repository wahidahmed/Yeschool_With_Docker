using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace School.AdminService.Data.Entities
{
    public class AcademicYear : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AcademicYearId { get; set; }
        public bool IsActive { get; set; } = false;

        public ICollection<StudentAcademicHistory> StudentAcademicHistory { get; set; } = new List<StudentAcademicHistory>();
        public ICollection<StudentInfo> StudentInfo { get; set; } = new List<StudentInfo>();
    }
}
