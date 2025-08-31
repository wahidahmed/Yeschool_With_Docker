using System.ComponentModel.DataAnnotations;

namespace School.AdminService.Data.Entities
{
    public class BaseEntity
    {

        public DateTime LastUpdatedOn { get; set; } = DateTime.UtcNow;
        public int LastUpdatedBy { get; set; } = 0;
    }
}
