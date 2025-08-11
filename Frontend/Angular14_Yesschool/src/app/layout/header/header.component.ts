import { DOCUMENT } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { UserCredStoreService } from 'src/app/services/user-cred-store.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(@Inject(DOCUMENT) private document: Document,  public  _router: Router,private userStore:UserCredStoreService,private auth:AuthService) { }

  userName?:string;
  role?:string;

  ngOnInit(): void {
    this.userStore.getUsernameFromStore().subscribe(val=>{
      let uName=this.auth.getuserNameFromToken();
      this.userName=val || uName;
    })
    this.userStore.getRoleFromStore().subscribe(val=>{
      let rl=this.auth.getRoleFromToken();
      this.role=val|| rl;
    })
  }
  sidebarToggle()
  {
    //toggle sidebar function
    this.document.body.classList.toggle('toggle-sidebar');
  }

  loggedIn(){
   this.userName= localStorage.getItem('userName');
   return this.userName;
  }
  logOut(){
    localStorage.removeItem('token');
    localStorage.removeItem('userName');
    this._router.navigate(['/login']);
  }
}