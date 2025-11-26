import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { IStudentInfoDto } from '../schoolAdmin/Dto/IStudentInfoDto';
import { IPersonInfoDto } from '../schoolAdmin/Dto/IPersonInfoDto';
import { IAddressDto } from '../schoolAdmin/Dto/IAddressDto';
import { API_BASE_URL } from '../app.tokens';
import { IGetStudentInfoDto } from '../schoolAdmin/Dto/IGetStudentInfoDto';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StudentAdmissionService {

 private baseUrl:string;
   constructor(private http:HttpClient,@Inject(API_BASE_URL) private apiBaseUrl: string) {
     this.baseUrl = `${apiBaseUrl}/api/admin_service`;
    }

   studentAdmission(stuDto:IStudentInfoDto,perDto:IPersonInfoDto,preAddDto:IAddressDto, perAddDto:IAddressDto ){
    let dto={
      studentInfo:stuDto,
      personalInfo:perDto,
      presentAddress:preAddDto,
      permanentAddress:perAddDto
    };
    return this.http.post<any>(`${this.baseUrl}/Admission`,dto);
   }

   getStudentList(personalId:number=0,studentId:number=0,classId:number=0,page:number=0,pageSize:number=0):Observable<IGetStudentInfoDto[]>{
    const params = {
    personalId: personalId,
    studentId: studentId,
    classId: classId,
    page: page,
    pageSize: pageSize
  };

  return this.http.get<IGetStudentInfoDto[]>(`${this.baseUrl}/StudentInfo`, { params });
   }
}
