namespace School.AdminService.DataTransferObjects
{
    public class ScheduleDto
    {
        public int ClassSectionId { get; set; }
        public int TotalPeriod { get; set; }
        public TimeOnly ScheduleStartTime { get; set; }
        public int DurationInMinutes { get; set; }
        public int ThePeriod_Before_TiffinBreak { get; set; }
        public int BreakMinutes { get; set; }

        public List<ScheduleDetailDto> scheduleDetailDtos { get; set; }
    }

    public class ScheduleUpdateDto
    {
        public int ScheduleMasterId { get; set; }
        public int ClassSectionId { get; set; }
        public int TotalPeriod { get; set; }
        public TimeOnly ScheduleStartTime { get; set; }
        public int DurationInMinutes { get; set; }
        public int ThePeriod_Before_TiffinBreak { get; set; }
        public int BreakMinutes { get; set; }

        public List<ScheduleDetailDto> scheduleDetailDtos { get; set; }
    }

    public class ScheduleDetailDto
    {
        public int ScheduleDetailId { get; set; }
        public int ScheduleMasterId { get; set; }
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
