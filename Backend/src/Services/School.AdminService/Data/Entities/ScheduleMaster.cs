using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.AdminService.Data.Entities
{
    public class ScheduleMaster:BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ScheduleMasterId {  get; set; }
        [ForeignKey("ClassSection")]
        public int ClassSectionId {  get; set; }
        public int TotalPeriod {  get; set; }
        public TimeOnly ScheduleStartTime { get; set; }
        public int DurationInMinutes {  get; set; }
        public int ThePeriod_Before_TiffinBreak { get; set; }
        public int BreakMinutes { get; set; }
        public ClassSection ClassSection { get; set; }
        public ICollection<ScheduleDetail> ScheduleDetails { get; set; }=new List<ScheduleDetail>();
    }
}
