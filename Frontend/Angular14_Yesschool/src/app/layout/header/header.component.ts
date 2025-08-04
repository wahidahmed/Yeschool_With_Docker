import { DOCUMENT } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(@Inject(DOCUMENT) private document: Document,  public  _router: Router) { }

  userName:string='';

  ngOnInit(): void {

  }
  sidebarToggle()
  {
    //toggle sidebar function
    this.document.body.classList.toggle('toggle-sidebar');
  }

  loggedIn(){
  //  this.userName= localStorage.getItem('userName');
   return this.userName;
  }
  logOut(){
    localStorage.removeItem('token');
    localStorage.removeItem('userName');
    this._router.navigate(['/']);
  }
}