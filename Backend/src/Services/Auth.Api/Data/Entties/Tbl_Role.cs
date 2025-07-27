using System.ComponentModel.DataAnnotations;

namespace Auth.Api.Data.Entties
{
    public class Tbl_Role
    {
        [Key]
        [StringLength(50)]
        [Required]
        public string Code { get; set; }

     
        [StringLength(200)]
        [Required]
        public string Name { get; set; }
        public bool? Status { get; set; }
    }
}
