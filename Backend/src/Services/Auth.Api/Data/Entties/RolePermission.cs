using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Api.Data.Entties
{
    public class RolePermission
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Userrole { get; set; }

        [StringLength(50)]
        [Required]
        public string Menucode { get; set; }

        public bool Haveview { get; set; }

        public bool Haveadd { get; set; }

        public bool Haveedit { get; set; }

        public bool Havedelete { get; set; }
    }
}
