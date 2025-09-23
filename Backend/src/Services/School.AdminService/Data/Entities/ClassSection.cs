using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.AdminService.Data.Entities
{
    public class ClassSection
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClassSectionId { get; set; }
        [Required]
        [StringLength(50)]
        public string ClassSectionName { get; set; }
       
        [ForeignKey("Classes")]
        public int ClassesId { get; set; }
      
        [ForeignKey("Section")]
        public int SectionId { get; set; }

        public Classes Classes { get; set; }
        public Section Section { get; set; }

        public ICollection<StudentAcademicHistory> StudentAcademicHistory { get; set; } = new List<StudentAcademicHistory>();
    }
}
