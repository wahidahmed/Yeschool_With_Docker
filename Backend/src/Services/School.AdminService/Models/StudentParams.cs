namespace School.AdminService.Models
{
    public class StudentParams
    {
        //long personalId = 0, long studentId = 0, int classId = 0, int page = 1, int pageSize = 5
        public long personalId { get; set; }
        public long studentId { get; set; }
        public int classId { get; set; }
        public int page { get; set; } = 0;
        public int pageSize { get; set; } = 0;
        public string searhTerm { get; set; } = "";

    }
}
