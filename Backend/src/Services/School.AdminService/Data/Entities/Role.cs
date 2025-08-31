using System.ComponentModel.DataAnnotations;

namespace School.AdminService.Data.Entities
{
    public class Role:BaseEntity
    {
        [Key]
        public int RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
