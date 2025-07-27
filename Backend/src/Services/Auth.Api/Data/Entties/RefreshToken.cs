using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Api.Data.Entties
{
    public class RefreshToken
    {
        [Key]
        [StringLength(50)]
        [Required]
        public string Userid { get; set; }

        [StringLength(50)]
        public string Tokenid { get; set; }

        [Column("refreshtoken")]
        public string Refreshtoken { get; set; }
    }
}
