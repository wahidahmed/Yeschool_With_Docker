using School.AdminService.Data.Entities;
using School.AdminService.DataTransferObjects;

namespace School.AdminService.Repository.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<GetStudentDetailDto>> GetStudentInfoAsync(long personalId = 0, long studentId = 0, int classId = 0, int page = 1,
    int pageSize = 10);
    }
}
