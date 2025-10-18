import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IStudentInfoDto } from '../schoolAdmin/Dto/IStudentInfoDto';
import { IPersonInfoDto } from '../schoolAdmin/Dto/IPersonInfoDto';
import { IAddressDto } from '../schoolAdmin/Dto/IAddressDto';

@Injectable({
  providedIn: 'root'
})
export class StudentAdmissionService {

 private baseUrl:string='http://localhost:5005/api/admin_service/';
   constructor(private http:HttpClient) { }

   studentAdmission(stuDto:IStudentInfoDto,perDto:IPersonInfoDto,preAddDto:IAddressDto, perAddDto:IAddressDto){
    let dto={
      studentInfo:stuDto,
      personalInfo:perDto,
      presentAddress:preAddDto,
      permanentAddress:perAddDto
    };
    return this.http.post<any>(`${this.baseUrl}/Admission`,dto);
   }
}
