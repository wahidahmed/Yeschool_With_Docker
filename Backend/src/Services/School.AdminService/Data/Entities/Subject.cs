using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.AdminService.Data.Entities
{
    public class Subject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SubjectId { get; set; }
        [Required]
        [MaxLength(100)]
        public string SubjectName { get; set; }
        public string Remarks {  get; set; }
    }
}
