using System.ComponentModel.DataAnnotations;

namespace School.AdminService.DataTransferObjects
{
    public class FeesNameDto
    {
        public int FeesNameID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Frequency { get; set; }
        public string Remarks { get; set; }
    }
}
