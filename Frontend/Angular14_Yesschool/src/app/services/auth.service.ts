import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from "@angular/common/http";
import {JwtHelperService} from '@auth0/angular-jwt';
import { TokenApiModel } from '../models/token-api.model';
@Injectable({
  providedIn: 'root'
})
export class AuthService {

 private baseUrl: string = 'http://localhost:5001/api/User/';
  private userPayload:any;
   private jwtHelper = new JwtHelperService();
  constructor(private http: HttpClient, private router: Router) {
    this.userPayload = this.decodedToken();
   }

   signUp(userObj: any) {
    return this.http.post<any>(`${this.baseUrl}register`, userObj)
  }

  signIn(loginObj : any){
    return this.http.post<any>(`${this.baseUrl}authenticate`,loginObj)
  }

  signOut(){
    localStorage.clear();
    this.router.navigate(['login'])
  }

  storeToken(tokenValue: string){
    localStorage.setItem('token', tokenValue)
  }
   storeUserName(username: string){
    localStorage.setItem('userName', username)
  }
  storeRefreshToken(tokenValue: string){
    localStorage.setItem('refreshToken', tokenValue)
  }

  getToken(){
    return localStorage.getItem('token')
  }
  getRefreshToken(){
    return localStorage.getItem('refreshToken')
  }

  isLoggedIn(): boolean{
    return !!localStorage.getItem('token');
  }

  decodedToken(){
    // const jwtHelper = new JwtHelperService();
    const token = this.getToken()!;
    console.log(this.jwtHelper.decodeToken(token))
    return this.jwtHelper.decodeToken(token)
  }

  getuserNameFromToken(){
    if(this.userPayload)
    return this.userPayload.name;
  }

  getRoleFromToken(){
    if(this.userPayload)
    return this.userPayload.role;
  }

  renewToken(tokenApi : TokenApiModel){
    return this.http.post<any>(`${this.baseUrl}refresh`, tokenApi)
  }

  getUsers(){
    return this.http.get<any>(this.baseUrl);
  }

    isAccessTokenExpired(): boolean {
    const token = this.getToken();
    return !token || this.jwtHelper.isTokenExpired(token);
  }

  isRefreshTokenExpired(): boolean {
    const token = this.getRefreshToken();
    console.log('ref',token)
     // If token is missing, treat it as expired
    if (!token || token.split('.').length !== 3) {
      return true;
    }
    return !token || this.jwtHelper.isTokenExpired(token);
  }
}
