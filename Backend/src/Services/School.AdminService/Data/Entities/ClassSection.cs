using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.AdminService.Data.Entities
{
    public class ClassSection:BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ClassSectionId { get; set; }
       
        [ForeignKey("Classes")]
        public int ClassesId { get; set; }
      
        [ForeignKey("Section")]
        public int SectionId { get; set; }

        public Classes Classes { get; set; }
        public Section Section { get; set; }

        public ICollection<StudentAcademicHistory> StudentAcademicHistory { get; set; } = new List<StudentAcademicHistory>();
    }
}
