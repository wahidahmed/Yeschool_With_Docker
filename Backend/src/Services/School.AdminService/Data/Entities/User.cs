using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.AdminService.Data.Entities
{
    public class User:BaseEntity
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }
        [MaxLength(200)]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public bool RemamberMe { get; set; }
        [NotMapped]
        public string Token { get; set; }
        [NotMapped]
        public string AppOriginUrl { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
