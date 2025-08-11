import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { catchError, map, Observable, of, tap } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { NgToastService } from 'ng-angular-popup';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private auth : AuthService, private router: Router, private toast: NgToastService){

  }
  // canActivate(){
  //   console.log('this.auth.isLoggedIn()',this.auth.isLoggedIn())
  //    if(this.auth.isLoggedIn()){
  //     return true;
  //   }else{
  //     this.toast.error({detail:"ERROR", summary:"Please Login First!"});
  //     this.router.navigate(['/login']);
  //     return false;
  //   }
  // }

  canActivate(): Observable<boolean> | boolean {
  if (!this.auth.isLoggedIn()) {
    this.toast.error({ detail: "ERROR", summary: "Please Login First!" });
    this.router.navigate(['/login']);
    return false;
  }

  if (!this.auth.isAccessTokenExpired()) {
    // Access token is still valid
    return true;
  }
  else{
      // Access token expired but refresh token is valid
    const refreshToken = this.auth.getRefreshToken();
    const accessToken = this.auth.getToken();
    return this.auth.renewToken({ accessToken, refreshToken }).pipe(
      tap(response => {
         this.auth.storeRefreshToken(response.refreshToken);
        this.auth.storeToken(response.accessToken);
      }),
      map(() => true),
      catchError(() => {
        this.toast.error({ detail: "ERROR", summary: "Session expired!" });
        this.router.navigate(['/login']);
        return of(false);
      })
    );
  }

  // Both tokens expired
  this.toast.error({ detail: "ERROR", summary: "Session expired!" });
  this.router.navigate(['/login']);
  return false;
}
  
}
