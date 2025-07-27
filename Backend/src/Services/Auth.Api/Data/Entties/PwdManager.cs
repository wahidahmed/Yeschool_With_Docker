using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Api.Data.Entties
{
    public class PwdManager
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Username { get; set; }

        [StringLength(200)]
        [Required]
        public string Password { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}
