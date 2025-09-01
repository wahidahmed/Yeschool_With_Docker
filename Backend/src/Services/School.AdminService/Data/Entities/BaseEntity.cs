using System.ComponentModel.DataAnnotations;

namespace School.AdminService.Data.Entities
{
    public class BaseEntity
    {
        public int CreatedBy {  get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
       
    }
}
