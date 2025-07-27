using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Api.Data.Entties
{
    public class OtpManager
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(10)]
        [Required]
        public string Otptext { get; set; } 

        [StringLength(20)]
        public string Otptype { get; set; }

        public DateTime Expiration { get; set; }

        public DateTime? Createddate { get; set; }
    }
}
