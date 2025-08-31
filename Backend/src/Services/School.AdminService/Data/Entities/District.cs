using System.ComponentModel.DataAnnotations.Schema;

namespace School.AdminService.Data.Entities
{
    public class District
    {
        public int DistrictId { get; set; }
        [ForeignKey("Division")]
        public int? DivisionsId { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }
        public Division Division { get; set; }
    }
}
