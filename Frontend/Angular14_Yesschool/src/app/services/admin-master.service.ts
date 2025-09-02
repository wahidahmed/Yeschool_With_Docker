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
      className:className,
      remarks:remarks
    }
    return this.http.post(`${this.baseUrl}Classes`,dto);
  }

  getClassList(){
    return this.http.get(`${this.baseUrl}classes`);
  }

  
}
