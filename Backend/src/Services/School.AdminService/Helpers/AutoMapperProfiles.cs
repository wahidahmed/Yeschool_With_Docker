using AutoMapper;
using School.AdminService.Data.Entities;
using School.AdminService.DataTransferObjects;

namespace School.AdminService.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Classes, ClassDto>().ReverseMap();
            CreateMap<Section, SectionDto>().ReverseMap();
            CreateMap<AcademicYear, AcademicYearDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<PersonalInfo, PersonalInfoDto>().ReverseMap();
            CreateMap<StudentInfo, StudentInfoDto>().ReverseMap();
            CreateMap<FeesName, FeesNameDto>().ReverseMap();
            CreateMap<FeesSetup, FeesSetupDto>().ReverseMap();
        }
    }
}
