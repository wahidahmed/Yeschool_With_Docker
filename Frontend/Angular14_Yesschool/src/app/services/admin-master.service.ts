import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { API_BASE_URL } from '../app.tokens';

@Injectable({
  providedIn: 'root'
})
export class AdminMasterService {
private baseUrl:string;
  constructor(private http:HttpClient, @Inject(API_BASE_URL) private apiBaseUrl: string) {
    this.baseUrl = `${apiBaseUrl}/api/admin_service/`;
   }

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
