using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.AdminService.Data.Entities
{
    public class StudentAcademicHistory:BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long StudentAcademicHistoryId { get; set; }

        [ForeignKey("StudentInfo")]
        public long StudentInfoId {  get; set; }

        [ForeignKey("ClassSection")]
        public int ClassSectionId { get; set; }

        [ForeignKey("AcademicYear")]
        public int AcademicYearId { get; set; }
        public ClassSection ClassSection { get; set; }
        public  AcademicYear AcademicYear { get; set; }
        public StudentInfo StudentInfo { get; set; }

        
    }
}
