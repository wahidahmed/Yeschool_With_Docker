using System.ComponentModel.DataAnnotations;

namespace School.AdminService.DataTransferObjects
{
    public class SectionDto
    {
        public int SectionId { get; set; }
        [Required(ErrorMessage = "Secion Name is required")]
        [RegularExpression(".*[a-zA-Z]+.*", ErrorMessage = "Numeric is not allowed")]
        public string SectionName { get; set; }
        public string Remarks { get; set; }
    }
}
