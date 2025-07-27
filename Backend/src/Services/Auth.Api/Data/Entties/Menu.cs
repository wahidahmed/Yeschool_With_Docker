using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Api.Data.Entties
{
    public class Menu
    {
        [Key]
        [StringLength(50)]
        [Required]
        public string Code { get; set; }

        [StringLength(200)]
        [Required]
        public string Name { get; set; }

        public bool? Status { get; set; }
    }
}
