using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace School.AdminService.Data.Entities
{
    public class Classes:BaseEntity
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ClassesId { get; set; }
        [Required]
        [MaxLength(50)]
        public string ClassesName { get; set; }
        [MaxLength(500)]
        public string Remarks { get; set; }

        public ICollection<ClassSection> ClassSection { get; set; } =new List<ClassSection>();

        public ICollection<StudentInfo> StudentInfo { get; set; } = new List<StudentInfo>();

        public ICollection<FeesSetup> FeesSetup { get; set;} =new List<FeesSetup>(); 
    }
}
