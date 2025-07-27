using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Api.Data.Entties
{
    public class RoleMenuMap
    {
        [Key]
        public int Id { get; set; }

        [StringLength(20)]
        [Required]
        public string Userrole { get; set; }

        [StringLength(50)]
        [Required]
        public string Menucode { get; set; }
    }
}
