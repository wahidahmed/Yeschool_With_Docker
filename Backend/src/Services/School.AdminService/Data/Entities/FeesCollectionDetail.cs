using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.AdminService.Data.Entities
{
    public class FeesCollectionDetail:BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long FeesCollectionDetailId { get; set; }

        [ForeignKey("FeesCollectionMaster")]
        public long FeeCollectMasterId { get; set; }

        [ForeignKey("FeesName")]
        public int FeesNameId { get; set; }

        public int? FromMonth { get; set; }

        public int? ToMonth { get; set; }

        public bool IsSpecial { get; set; }

        public decimal FeesAmount { get; set; }

        public decimal PartialDiscount { get; set; }

        public decimal TotalAmount { get; set; }

        public string Remarks { get; set; }

        public FeesCollectionMaster FeesCollectionMaster { get; set; }
        public FeesName FeesName { get; set; }
    }
}
