using System.ComponentModel.DataAnnotations;

namespace School.AdminService.Data.Entities
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }
    }
}
