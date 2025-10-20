namespace School.AdminService.Repository.Interfaces
{
    public interface IIdGeneratorService
    {
        Task<long> GetNextIdAsync(string entityType);
    }
}
