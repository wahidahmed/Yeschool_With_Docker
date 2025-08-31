using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.AdminService.Data.Entities
{
    public class FeesName:BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeesNameID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        
        public string Frequency { get; set; }
        public string Remarks { get; set; }

        public ICollection<FeesSetup> FeesSetup { get; set; } = new List<FeesSetup>();
    }
}
