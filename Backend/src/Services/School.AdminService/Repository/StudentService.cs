using Microsoft.EntityFrameworkCore;
using School.AdminService.Data;
using School.AdminService.Data.Entities;
using School.AdminService.DataTransferObjects;
using School.AdminService.Repository.Interfaces;

namespace School.AdminService.Repository
{
    public class StudentService: IStudentService
    {
        private readonly AppDbContext context;

        public StudentService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<GetStudentDetailDto>> GetStudentInfoAsync(long personalId=0,long studentId=0,int classId=0)
        {
            var query = context.StudentInfos
                                .Include(x=>x.Classes)

                                .Include(x=>x.studentAcademicHistories)
                                    .ThenInclude(p=>p.ClassSection)
                                        .ThenInclude(p=>p.Section)

                                .Include(x => x.PersonalInfo)
                                    .ThenInclude(p=>p.Addresses)
                                        .ThenInclude(q=>q.District)

                                .Include(x => x.PersonalInfo)
                                    .ThenInclude(p => p.Addresses)
                                        .ThenInclude(q => q.Thana)

                                .Select(s =>new GetStudentDetailDto
                                {
                                    StudentId=s.StudentId,
                                    PersonalInfoId=s.PersonalInfoId,
                                    GuardianName=s.GuardianName,
                                    GuardianMobileNo=s.GuardianMobileNo,
                                    GuardianRelation=s.GuardianRelation,
                                    Status=s.Status.ToString(),
                                    ClassesId=s.ClassesId,
                                    ClassesName=s.Classes.ClassesName,
                                    AcademicYearId=s.AcademicYearId,
                                    
                                    PersonName=s.PersonalInfo.PersonName,
                                    MobileNo=s.PersonalInfo.MobileNo,
                                    EmailAddress=s.PersonalInfo.EmailAddress,
                                    DateOfBirth=s.PersonalInfo.DateOfBirth,
                                    FatherName=s.PersonalInfo.FatherName,
                                    FatherMobile=s.PersonalInfo.FatherMobile,
                                    FatherOccupation=s.PersonalInfo.FatherOccupation,
                                    MotherName=s.PersonalInfo.MotherName,
                                    MotherMobile=s.PersonalInfo.MotherMobile,
                                    MotherOccupation=s.PersonalInfo.MotherOccupation,
                                    Gender=s.PersonalInfo.Gender,
                                    Religion=s.PersonalInfo.Religion,
                                    PersonCode=s.PersonalInfo.PersonCode,
                                    ImageUrl=s.PersonalInfo.ImageUrl,

                                    PresentAddress=s.PersonalInfo.Addresses.Where(x=>x.AddressType== "presnt").Select(a=>new AddressUpdateDto
                                    {
                                        AddressId=a.AddressId,
                                        AddressType=a.AddressType,
                                        DistrictId=a.DistrictId,
                                        StreetDetail=a.StreetDetail,
                                        ThanaId=a.ThanaId,
                                        ThanaName=a.Thana.ThanaName,
                                        DistrictName=a.District.DistrictName
                                    }).FirstOrDefault(),

                                    PermanentAddress=s.PersonalInfo.Addresses.Where(X=>X.AddressType== "permanent").Select(a=>new AddressUpdateDto
                                    {
                                        AddressId = a.AddressId,
                                        AddressType = a.AddressType,
                                        DistrictId = a.DistrictId,
                                        StreetDetail = a.StreetDetail,
                                        ThanaId = a.ThanaId,
                                        ThanaName = a.Thana.ThanaName,
                                        DistrictName = a.District.DistrictName
                                    }).FirstOrDefault(),
                                })
                                .AsQueryable();
            if(studentId > 0)
                query = query.Where(x => x.StudentId == studentId);
            if(personalId > 0)
                query=query.Where(x=>x.PersonalInfoId == personalId);
            if(classId > 0)
                query=query.Where(x=>x.ClassesId == classId);

            return await query.ToListAsync();
        }
    }
}
