using School.AdminService.Data.Entities;

namespace School.AdminService.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Classes> Classes { get; }
        IGenericRepository<Section> Section { get; }
        IGenericRepository<ClassSection> ClassSection { get; }
        IGenericRepository<AcademicYear> AcademicYear { get; }
        IGenericRepository<StudentInfo> StudentInfo { get; }
        IGenericRepository<PersonalInfo> PersonalInfo { get; }
        IGenericRepository<Address> Address { get; }
        IGenericRepository<District> District { get; }
        IGenericRepository<Thana> Thana { get; }
        IGenericRepository<FeesName> FeesName { get; }
        IGenericRepository<FeesSetup> FeesSetup { get; }
        IGenericRepository<FeesCollectionMaster> FeesCollectionMaster { get; }
        IGenericRepository<FeesCollectionDetail> FeesCollectionDetail { get; }


        Task<bool> SaveAsync();
    }
}
