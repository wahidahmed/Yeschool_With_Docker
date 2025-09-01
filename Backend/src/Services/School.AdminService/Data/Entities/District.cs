using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.AdminService.Data.Entities
{
    public class District
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DistrictId { get; set; }
        [ForeignKey("Division")]
        public int? DivisionsId { get; set; }
        [Required]
        [MaxLength(100)]
        public string DistrictName { get; set; }
        [MaxLength(500)]
        public string Remarks { get; set; }
        public Division Division { get; set; }
    }
}
