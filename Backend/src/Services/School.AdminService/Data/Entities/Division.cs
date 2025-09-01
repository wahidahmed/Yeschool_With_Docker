using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.AdminService.Data.Entities
{
    public class Division
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DivisionId { get; set; }
        [ForeignKey("Country")]
        public int CountriesId { get; set; }
        [Required]
        [MaxLength(100)]
        public string DivisionName { get; set; }
        public Country Country { get; set; }
    }
}
