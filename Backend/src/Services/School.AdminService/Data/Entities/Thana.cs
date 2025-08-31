using System.ComponentModel.DataAnnotations.Schema;

namespace School.AdminService.Data.Entities
{
    public class Thana
    {
        public int ThanaId { get; set; }
        [ForeignKey("District")]
        public int? DistrictsId { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }
        public District District { get; set; }
    }
}
