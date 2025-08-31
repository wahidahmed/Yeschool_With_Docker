using AutoMapper.Configuration.Conventions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.AdminService.Data.Entities
{
    public class StudentAcademicHistory:BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int64 StudentAcademicHistoryId { get; set; }

        //[ForeignKey("StudentInfo")]
        public Int64 StudentInfoId { get; set; }

        [ForeignKey("ClassSection")]
        public int ClassSectionId { get; set; }

        [ForeignKey("AcademicYear")]
        public int AcademicYearId { get; set; }

        public ClassSection ClassSection { get; set; }
        public StudentInfo StudentInfo { get; set; }
        public  AcademicYear AcademicYear { get; set; }

        
    }
}
