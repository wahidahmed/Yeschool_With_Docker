using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.AdminService.Data.Entities
{
    public class FeesSetup:BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long FeesSetupId { get; set; }

        [ForeignKey("Classes")]
        public int ClassesId { get;set; }

        [ForeignKey("FeesName")]
        public int FeesNameId { get; set; }
        public decimal Amount { get; set; }
        [DataType(DataType.Date)]
        public DateTime AppliedDate { get; set; }

        public Classes Classes { get; set; }
        public FeesName FeesName { get; set; }
    }
}
