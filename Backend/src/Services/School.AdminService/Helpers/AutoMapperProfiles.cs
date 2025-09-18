using AutoMapper;
using School.AdminService.Data.Entities;
using School.AdminService.DataTransferObjects;

namespace School.AdminService.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // 🔧 Add this line: string → DateOnly conversion
            CreateMap<string, DateOnly>()
                .ConvertUsing((string src) =>
                    string.IsNullOrEmpty(src) ? default(DateOnly) : DateOnly.Parse(src));


            CreateMap<Classes, ClassDto>().ReverseMap();
            CreateMap<Section, SectionDto>().ReverseMap();
            CreateMap<AcademicYear, AcademicYearDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<PersonalInfo, PersonalInfoDto>().ReverseMap();
            CreateMap<StudentInfo, StudentInfoDto>().ReverseMap();
            CreateMap<FeesName, FeesNameDto>().ReverseMap();
            CreateMap<FeesSetup, FeesSetupDto>().ReverseMap();
            CreateMap<FeesCollectionMaster,FeesCollectionDto>().ReverseMap();
            CreateMap<FeesCollectionDetail, FeesDetailsDto>().ReverseMap();

            CreateMap<AddressUpdateDto, Address>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcValue) => ShouldMap(srcValue)));

            CreateMap<PersonalInfoUpdateDto, PersonalInfo>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcValue) => ShouldMap(srcValue)));

            CreateMap<StudentInfoUpdateDto, StudentInfo>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcValue) => ShouldMap(srcValue)));

            CreateMap<FeesNameUpdateDto, FeesName>()
                .ForAllMembers(opt => opt.Condition ((src, dest, srcValue) => ShouldMap(srcValue)));
        }

        // Reusable condition logic
        private bool ShouldMap(object sourceValue)
        {
            if (sourceValue == null)
                return false;

            // Skip empty/whitespace strings
            if (sourceValue is string str && string.IsNullOrWhiteSpace(str))
                return false;

            // Skip default value types (e.g., 0, DateTime.MinValue)
            var type = sourceValue.GetType();
            if (type.IsValueType)
            {
                var defaultValue = Activator.CreateInstance(type);
                if (sourceValue.Equals(defaultValue))
                    return false;

                // Special handling for DateTime
                if (sourceValue is DateTime dt && dt == DateTime.MinValue)
                    return false;

                // Optional: handle DateOnly
                if (sourceValue is DateOnly d && d == DateOnly.MinValue)
                    return false;

                // Handle Guid.Empty
                if (sourceValue is Guid guid && guid == Guid.Empty)
                    return false;
            }

            return true;
        }
    }
}
