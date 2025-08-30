import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { catchError, Observable, switchMap, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { NgToastService } from 'ng-angular-popup';
import { Router } from '@angular/router';
import { TokenApiModel } from '../models/token-api.model';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(private auth:AuthService,private toast:NgToastService,private router:Router) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    // const myToken=this.auth.getToken();
    // if(myToken){
    //   request=request.clone({
    //     setHeaders:{Authorization:`Bearer ${myToken}`}
    //   })
    // }

     let authReq = request;
    const token = this.auth.getToken();

    if (token) {
      authReq = request.clone({
        setHeaders: { Authorization: `Bearer ${token}` }
      });
    }


    // return next.handle(request).pipe(
    //   catchError((err:any)=>{
    //     if(err instanceof HttpErrorResponse){
    //       if(err.status===401){
    //         // this.toast.warning({detail:'unautorized',summary:err.message,duration:5000});
    //         // this.router.navigate(['/login']);
    //         return this.handleUnAuthorizedError(request,next);
    //       }
    //       else if(err.status===400){
           
    //         return throwError(()=>new Error(err.error.message));
    //       }
    //     }
    //    return throwError(()=>new Error('unknown error'));
    //   })
    // );

     return next.handle(authReq).pipe(
      catchError(error => {
        if (error.status === 401 && !request.url.includes('authenticate') && !request.url.includes('refresh')) {
          const refreshToken = this.auth.getRefreshToken();
          if (refreshToken) {
            return this.auth.renewToken({
              accessToken: token!,
              refreshToken: refreshToken,
              username: localStorage.getItem('userName')!
            }).pipe(
              switchMap((newTokens: any) => {
                this.auth.storeToken(newTokens.accessToken);
                this.auth.storeRefreshToken(newTokens.refreshToken);
                const cloned = request.clone({
                  setHeaders: { Authorization: `Bearer ${newTokens.accessToken}` }
                });
                return next.handle(cloned);
              })
            );
          }
        }
        return throwError(() => error);
      })
    );

  }
  //    handleUnAuthorizedError(req: HttpRequest<any>, next: HttpHandler){
  //   let tokeApiModel = new TokenApiModel();
  //   tokeApiModel.accessToken = this.auth.getToken()!;
  //   tokeApiModel.refreshToken = this.auth.getRefreshToken()!;
  //   return this.auth.renewToken(tokeApiModel)
  //   .pipe(
  //     switchMap((data:TokenApiModel)=>{
  //       this.auth.storeRefreshToken(data.refreshToken);
  //       this.auth.storeToken(data.accessToken);
  //       req = req.clone({
  //         setHeaders: {Authorization:`Bearer ${data.accessToken}`}  // "Bearer "+myToken
  //       })
  //       return next.handle(req);
  //     }),
  //     catchError((err)=>{
  //       return throwError(()=>{
  //         this.toast.warning({detail:"Warning", summary:"Token is expired, Please Login again"});
  //         this.router.navigate(['login'])
  //       })
  //     })
  //   )
  // }



}
