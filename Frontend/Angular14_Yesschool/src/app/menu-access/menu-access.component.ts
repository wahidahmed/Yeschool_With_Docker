import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { AdminitrationService } from '../services/adminitration.service';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-menu-access',
  templateUrl: './menu-access.component.html',
  styleUrls: ['./menu-access.component.css']
})
export class MenuAccessComponent implements OnInit {

  constructor(private fb:FormBuilder,private adminstrationService:AdminitrationService, private toast:NgToastService) { }
  menuList:any[];
  ngOnInit(): void {
  }

  getMenus(role:string){
    this.adminstrationService.getMenusByRole(role).subscribe(data=>{
      
      this.menuList=data;
    })
  }
}
