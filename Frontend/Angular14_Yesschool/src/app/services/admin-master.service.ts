import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AdminMasterService {
private baseUrl:string='http://localhost:5005/api/admin_service/';
  constructor(private http:HttpClient) { }

  addNewClass(className:string,remarks:string){
    let dto={
      ClassesName:className,
      Remarks:remarks
    }
    return this.http.post<any>(`${this.baseUrl}Classes`,dto);
  }

  

  getClassList(){
    return this.http.get<any>(`${this.baseUrl}classes`);
  }

  addNewSection(sectionName:string,remarks:string){
    let dto={
      SectionName:sectionName,
      Remarks:remarks
    }
    return this.http.post<any>(`${this.baseUrl}Section`,dto);
  }
  getSectionList(){
    return this.http.get<any>(`${this.baseUrl}Section`);
  }

  addFeesName(feesName:string,feesCollectionType:string,remarks:string){
    let dto={
      Name:feesName,
      FeesCollectionType:feesCollectionType,
      Remarks:remarks
    }
    return this.http.post<any>(`${this.baseUrl}Fees/AddFeesName`,dto);
  }

  getFeesNameList(){
    return this.http.get<any>(`${this.baseUrl}Fees`);
  }
  
}
