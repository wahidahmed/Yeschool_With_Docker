import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { IStudentInfoDto } from '../schoolAdmin/Dto/IStudentInfoDto';
import { IPersonInfoDto } from '../schoolAdmin/Dto/IPersonInfoDto';
import { IAddressDto } from '../schoolAdmin/Dto/IAddressDto';
import { API_BASE_URL } from '../app.tokens';

@Injectable({
  providedIn: 'root'
})
export class StudentAdmissionService {

 private baseUrl:string;
   constructor(private http:HttpClient,@Inject(API_BASE_URL) private apiBaseUrl: string) {
     this.baseUrl = `${apiBaseUrl}/api/admin_service/`;
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
}
