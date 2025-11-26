import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { IAssignAccess } from '../models/iAssignAccess.model';
import { API_BASE_URL } from '../app.tokens';

@Injectable({
  providedIn: 'root'
})
export class AdminitrationService {

   private baseUrl:string;
  constructor(private http:HttpClient,@Inject(API_BASE_URL) private apiBaseUrl: string) {
     this.baseUrl = `${apiBaseUrl}/api/auth/admin/`;
   }

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
