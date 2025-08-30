import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IAssignAccess } from '../models/iAssignAccess.model';

@Injectable({
  providedIn: 'root'
})
export class AdminitrationService {

   private baseUrl: string = 'http://localhost:5001/api/Administration/';
  constructor(private http:HttpClient) { }

  addNnewRole(roleName:string){
    return this.http.post<any>(`${this.baseUrl}AddNewRole?roleName=${roleName}`,{});
  }

  assignAccess(dto:IAssignAccess){
    return this.http.post<any>(`${this.baseUrl}AssignAccess`,dto);
  }

   getRoles(){
    return this.http.get<any>(`${this.baseUrl}GetAllRoles`);
  }
   GetAppContentByRole(role:string){
    return this.http.get<any>(`${this.baseUrl}GetAppContentByRole`,{ params: { roleName: role } });
  }

  getMenuItem(userName:string){
    return this.http.get<any>(`${this.baseUrl}GetMenuItem`,{params:{userName:userName}});
  }
}
