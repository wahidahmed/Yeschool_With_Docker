import { IAddressDto } from "./IAddressDto";

export interface IGetStudentInfoDto{
    AcademicYearId?:number;
    ClassesId?:number;
    PersonalInfoId?:number;
    SectionId?:number;
    StudentId?:number;
    
    ClassesName?:string;
    EmailAddress?:string;
    FatherMobile?:string;
    FatherName?:string;
    FatherOccupation?:string;
    Gender?:string;
    GuardianMobileNo?:string;
    GuardianName?:string;
    GuardianRelation?:string;
    ImageUrl?:string;
    MobileNo?:string;
    MotherMobile?:string;
    MotherName?:string;
    MotherOccupation?:string;
    PersonCode?:string;
    PersonName?:string;
    Religion?:string;
    SectionName?:string;
    Status?:string;
    DateOfBirth?:Date;
    IsActive?:boolean;
    PermanentAddress?:IAddressDto;
    PresentAddress?:IAddressDto;
}