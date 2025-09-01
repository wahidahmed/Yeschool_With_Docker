using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.AdminService.Data.Entities
{
    public class FeesName:BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeesNameId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string FeesCollectionType { get; set; }// exp:monthly, daily, yearly
        [MaxLength(500)]
        public string Remarks { get; set; }

        public ICollection<FeesSetup> FeesSetup { get; set; } = new List<FeesSetup>();
    }
}
