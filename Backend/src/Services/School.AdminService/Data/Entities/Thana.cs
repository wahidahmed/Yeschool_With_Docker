using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.AdminService.Data.Entities
{
    public class Thana
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ThanaId { get; set; }
        [ForeignKey("District")]
        public int? DistrictsId { get; set; }
        [Required]
        [MaxLength(100)]
        public string ThanaName { get; set; }
        [MaxLength(500)]
        public string Remarks { get; set; }
        public District District { get; set; }
    }
}
