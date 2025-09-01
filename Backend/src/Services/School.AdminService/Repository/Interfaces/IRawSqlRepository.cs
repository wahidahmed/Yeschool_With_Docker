using School.AdminService.Data.Entities;

namespace School.AdminService.Repository.Interfaces
{
    public interface IRawSqlRepository
    {
        void ExecuteSqlRaw(string sql, params object[] param);
        Task<IEnumerable<RawSqlColumns>> Vw_StudentInfo();
        Task<IEnumerable<RawSqlColumns>> Get_RecentFeesByClassId(int classId = 0);
        RawSqlColumns GetFeesInvoice();
    }
}
