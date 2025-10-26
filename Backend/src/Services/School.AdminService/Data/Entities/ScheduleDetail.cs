using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.AdminService.Data.Entities
{
    public class ScheduleDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ScheduleDetailId { get; set; }
        [ForeignKey("ScheduleMaster")]
        public int ScheduleMasterId { get; set; }
        [ForeignKey("Teacher")]
        public int TeacherId {  get; set; }
        [ForeignKey("Subject")]
        public int SubjectId {  get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public ScheduleMaster ScheduleMaster { get; set; }
        public Employee Teacher { get; set; }
        public Subject Subject { get; set; }
    }
}
