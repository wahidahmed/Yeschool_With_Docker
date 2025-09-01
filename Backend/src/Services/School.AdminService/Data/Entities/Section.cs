using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace School.AdminService.Data.Entities
{
    public class Section:BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SectionId { get; set; }
        [Required]
        [MaxLength(50)]
        public string SectionName { get; set; }
        [MaxLength(500)]
        public string Remarks { get; set; }
        public ICollection<ClassSection> ClassSection { get; set; } = new List<ClassSection>();
    }
}
