using Microsoft.EntityFrameworkCore;
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
        public long FeesCollectionMasterId { get; set; }

        [ForeignKey("FeesName")]
        public int FeesNameId { get; set; }

        public int? FromMonth { get; set; }

        public int? ToMonth { get; set; }

        public bool IsSpecial { get; set; }=false;
        [Precision(18, 2)]
        public decimal FeesAmount { get; set; }
        [Precision(18, 2)]
        public decimal Discount { get; set; } = 0;
        [Precision(18, 2)]
        public decimal TotalAmount { get; set; }
        [MaxLength(500)]
        public string Remarks { get; set; }

        public FeesCollectionMaster FeesCollectionMaster { get; set; }
        public FeesName FeesName { get; set; }
    }
}
