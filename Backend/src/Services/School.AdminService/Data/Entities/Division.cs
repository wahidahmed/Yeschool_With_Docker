using System.ComponentModel.DataAnnotations.Schema;

namespace School.AdminService.Data.Entities
{
    public class Division
    {
        public int DivisionId { get; set; }
        [ForeignKey("Country")]
        public int CountriesId { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }
    }
}
