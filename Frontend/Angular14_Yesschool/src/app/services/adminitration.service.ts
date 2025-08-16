import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AdminitrationService {

   private baseUrl: string = 'http://localhost:5001/api/Administration/';
  constructor(private http:HttpClient) { }

  addNnewRole(roleName:string){
    return this.http.post<any>(`${this.baseUrl}AddNewRole`,roleName);
  }

   getRoles(){
    return this.http.get<any>(`${this.baseUrl}GetAllRoles`);
  }
   GetAppContentByRole(role:string){
    return this.http.get<any>(`${this.baseUrl}GetAppContentByRole`,{ params: { roleName: role } });
  }
}
