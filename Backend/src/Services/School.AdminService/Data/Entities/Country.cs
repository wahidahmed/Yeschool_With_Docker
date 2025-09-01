using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.AdminService.Data.Entities
{
    public class Country
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CountryId { get; set; }
        [Required]
        [MaxLength(100)]
        public string CountryName { get; set; }
        [MaxLength(500)]
        public string Remarks { get; set; }
    }
}
