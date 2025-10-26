using System.ComponentModel.DataAnnotations;

namespace School.AdminService.DataTransferObjects
{
    public class SubjectDto
    {
        public int SubjectId {  get; set; }
        [Required]
        [MaxLength(100)]
        public string SubjectName { get; set; }
        public string Remarks { get; set; }
    }
}
