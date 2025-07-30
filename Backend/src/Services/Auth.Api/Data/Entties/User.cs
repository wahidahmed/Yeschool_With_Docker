using System.ComponentModel.DataAnnotations;

namespace Auth.Api.Data.Entties
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Username { get; set; }
        [Required]
        [MaxLength(500)]
        public string Password { get; set; }
        public string Token { get; set; }
        [Required]
        [MaxLength(100)]
        public string Role { get; set; }
        public string Email { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
