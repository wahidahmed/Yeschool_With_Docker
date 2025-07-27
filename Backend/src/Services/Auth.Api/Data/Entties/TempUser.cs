using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Api.Data.Entties
{
    public class TempUser
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Code { get; set; }
        [StringLength(250)]
        [Required]
        public string Name { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Password { get; set; }
    }
}
