using System.ComponentModel.DataAnnotations;

namespace School.AdminService.DataTransferObjects
{
    public class FeesNameDto
    {
        public int FeesNameId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string FeesCollectionType { get; set; }// exp:monthly, daily, yearly
        [MaxLength(500)]
        public string Remarks { get; set; }
    }
    public class FeesNameUpdateDto
    {
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string FeesCollectionType { get; set; }// exp:monthly, daily, yearly
        [MaxLength(500)]
        public string Remarks { get; set; }
    }
}
