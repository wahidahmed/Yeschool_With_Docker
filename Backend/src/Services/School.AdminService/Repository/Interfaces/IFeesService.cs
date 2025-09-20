using School.AdminService.Data.Entities;
using School.AdminService.DataTransferObjects;

namespace School.AdminService.Repository.Interfaces
{
    public interface IFeesService
    {
        Task<IEnumerable<GetFeesDetailsDto>> GetFeesDetails(long studentId = 0, long feesId = 0, int fromMonth = 0, int toMonth = 0);
    }
}
