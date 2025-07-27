using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Api.Data.Entties
{
    public class Tbl_User
    {
        [Key]
        [StringLength(50)]
        [Required]
        public string Username { get; set; }

       
        [StringLength(250)]
        [Required]
        public string Name { get; set; }

     
        [StringLength(100)]
        public string Email { get; set; }

      
        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

      
        public bool? Isactive { get; set; }

        [StringLength(50)]
        public string Role { get; set; }

        public bool? Islocked { get; set; }

        public int? Failattempt { get; set; }
    }
}
