using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace School.AdminService.DataTransferObjects
{
    public class FeesSetupDto
    {
        public long FeesSetupId { get; set; }
        public int ClassesId { get; set; }
        public int FeesNameId { get; set; }
        public decimal Amount { get; set; }
        public string AppliedDate { get; set; }
    }
}
