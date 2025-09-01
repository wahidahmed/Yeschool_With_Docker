using System.ComponentModel.DataAnnotations;

namespace School.AdminService.DataTransferObjects
{
    public class ClassDto
    {
        public int ClassID { get; set; }
        [Required(ErrorMessage ="Class Name is required")]
        [RegularExpression(".*[a-zA-Z]+.*",ErrorMessage ="Class Name cannot contain numeric value")]
        public string ClassName { get; set; }
       
        public string Remarks { get; set; }
    }
}
