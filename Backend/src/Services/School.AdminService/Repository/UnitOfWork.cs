using School.AdminService.Data;
using School.AdminService.Data.Entities;
using School.AdminService.Repository.Interfaces;
using System.Runtime.InteropServices.JavaScript;

namespace School.AdminService.Repository
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        // ✅ Private fields to store repository instances
        private IGenericRepository<Classes> _classes;
        private IGenericRepository<Section> _section;
        private IGenericRepository<ClassSection> _classSection;
        private IGenericRepository<AcademicYear> _academicYear;
        private IGenericRepository<StudentInfo> _studentInfo;
        private IGenericRepository<PersonalInfo> _personalInfo;
        private IGenericRepository<Address> _address;
        private IGenericRepository<District> _district;
        private IGenericRepository<Thana> _thana;
        private IGenericRepository<FeesName> _feesName;
        private IGenericRepository<FeesSetup> _feesSetup;
        private IGenericRepository<FeesCollectionMaster> _feesCollectionMaster;
        private IGenericRepository<FeesCollectionDetail> _feesCollectionDetail;
        private IGenericRepository<StudentAcademicHistory> _studentAcademicHistory;
        private IGenericRepository<Subject> _subject;
        private IGenericRepository<Employee> _employee;
        private IGenericRepository<ScheduleMaster> _scheduleMaster;
        private IGenericRepository<ScheduleDetail> _scheduleDetail;
        
        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /* ✅ Lazy initialization: create ONCE, reuse forever*/

            //public IGenericRepository<Classes> Classes()
            //{
            //    if (_classes == null)
            //    {
            //        _classes = new GenericRepository<Classes>(_dbContext);
            //    }
            //    return _classes;
            //}
        public IGenericRepository<Classes> Classes => _classes ??= new GenericRepository<Classes>(_dbContext);
        public IGenericRepository<Section> Section => _section ??= new GenericRepository<Section>(_dbContext);
        public IGenericRepository<ClassSection> ClassSection => _classSection ??= new GenericRepository<ClassSection>(_dbContext);
        public IGenericRepository<AcademicYear> AcademicYear => _academicYear ??= new GenericRepository<AcademicYear>(_dbContext);
        public IGenericRepository<StudentInfo> StudentInfo => _studentInfo ??= new GenericRepository<StudentInfo>(_dbContext);
        public IGenericRepository<PersonalInfo> PersonalInfo => _personalInfo ??= new GenericRepository<PersonalInfo>(_dbContext);
        public IGenericRepository<Address> Address => _address ??= new GenericRepository<Address>(_dbContext);
        public IGenericRepository<District> District => _district ??= new GenericRepository<District>(_dbContext);
        public IGenericRepository<Thana> Thana => _thana ??= new GenericRepository<Thana>(_dbContext);
        public IGenericRepository<FeesName> FeesName => _feesName ??= new GenericRepository<FeesName>(_dbContext);
        public IGenericRepository<FeesSetup> FeesSetup => _feesSetup ??= new GenericRepository<FeesSetup>(_dbContext);
        public IGenericRepository<FeesCollectionMaster> FeesCollectionMaster => _feesCollectionMaster ??= new GenericRepository<FeesCollectionMaster>(_dbContext);
        public IGenericRepository<FeesCollectionDetail> FeesCollectionDetail => _feesCollectionDetail ??= new GenericRepository<FeesCollectionDetail>(_dbContext);
        public IGenericRepository<StudentAcademicHistory> StudentAcademicHistory => _studentAcademicHistory ??= new GenericRepository<StudentAcademicHistory>(_dbContext);
        public IGenericRepository<Subject> Subject => _subject ??= new GenericRepository<Subject>(_dbContext);
        public IGenericRepository<Employee> Employee => _employee ??= new GenericRepository<Employee>(_dbContext);
        public IGenericRepository<ScheduleMaster> ScheduleMaster => _scheduleMaster ??= new GenericRepository<ScheduleMaster>(_dbContext);
        public IGenericRepository<ScheduleDetail> ScheduleDetail => _scheduleDetail ??= new GenericRepository<ScheduleDetail>(_dbContext);

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
