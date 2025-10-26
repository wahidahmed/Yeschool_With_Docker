using School.AdminService.Data.Entities;
using School.AdminService.Data;
using School.AdminService.Repository.Interfaces;

namespace School.AdminService.Repository
{
    public class UnitOfWork: IUnitOfWork
    {
        private AppDbContext _dbContext;
        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<Classes> Classes => new GenericRepository<Classes>(_dbContext);
        public IGenericRepository<Section> Section => new GenericRepository<Section>(_dbContext);
        public IGenericRepository<ClassSection> ClassSection => new GenericRepository<ClassSection>(_dbContext);
        public IGenericRepository<AcademicYear> AcademicYear => new GenericRepository<AcademicYear>(_dbContext);
        public IGenericRepository<StudentInfo> StudentInfo => new GenericRepository<StudentInfo>(_dbContext);
        public IGenericRepository<PersonalInfo> PersonalInfo => new GenericRepository<PersonalInfo>(_dbContext);
        public IGenericRepository<Address> Address => new GenericRepository<Address>(_dbContext);
        public IGenericRepository<District> District => new GenericRepository<District>(_dbContext);
        public IGenericRepository<Thana> Thana => new GenericRepository<Thana>(_dbContext);
        public IGenericRepository<FeesName> FeesName => new GenericRepository<FeesName>(_dbContext);
        public IGenericRepository<FeesSetup> FeesSetup => new GenericRepository<FeesSetup>(_dbContext);
        public IGenericRepository<FeesCollectionMaster> FeesCollectionMaster => new GenericRepository<FeesCollectionMaster>(_dbContext);
        public IGenericRepository<FeesCollectionDetail> FeesCollectionDetail => new GenericRepository<FeesCollectionDetail>(_dbContext);
        public IGenericRepository<StudentAcademicHistory> StudentAcademicHistory => new GenericRepository<StudentAcademicHistory>(_dbContext);
        public IGenericRepository<Subject> Subject => new GenericRepository<Subject>(_dbContext);
        public IGenericRepository<Employee> Employee => new GenericRepository<Employee>(_dbContext);
        public IGenericRepository<ScheduleMaster> ScheduleMaster => new GenericRepository<ScheduleMaster>(_dbContext);
        public IGenericRepository<ScheduleDetail> ScheduleDetail => new GenericRepository<ScheduleDetail>(_dbContext);
        public async Task<bool> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
