using System.ComponentModel.DataAnnotations;

namespace Auth.Api.Data.Entties
{
    public class Role
    {
        public int Id { get; set; }
        [MaxLength(255)]
        [Required]
        public string RoleName { get; set; }
    }
}
