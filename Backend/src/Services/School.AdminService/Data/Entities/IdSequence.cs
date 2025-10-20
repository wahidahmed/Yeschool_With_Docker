using System.ComponentModel.DataAnnotations;

namespace School.AdminService.Data.Entities
{
    public class IdSequence
    {
        [Key]
        public string EntityType { get; set; } = null!; // e.g., "PersonalInfo", "Student"
        public long NextId { get; set; }
    }
}
