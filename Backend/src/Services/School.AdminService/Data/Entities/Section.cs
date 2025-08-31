using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace School.AdminService.Data.Entities
{
    public class Section:BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SectionID { get; set; }
        [Required]
        public string SectionName { get; set; }
        public string Remarks { get; set; }
        public ICollection<ClassSection> ClassSection { get; set; } = new List<ClassSection>();
    }
}
