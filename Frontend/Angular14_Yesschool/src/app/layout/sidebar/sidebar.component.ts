import { Component, OnInit } from '@angular/core';
import { AdminitrationService } from 'src/app/services/adminitration.service';
import { AuthService } from 'src/app/services/auth.service';
import { UserCredStoreService } from 'src/app/services/user-cred-store.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {

  userNameFromSidebar:string;
  roleFromSidebar:string;
  menuList:any[];
  parentMenu:any[];
  childrenMenu:any[];
  constructor(private auth:AuthService,private adminService:AdminitrationService) { }

  ngOnInit(): void {
    this.roleFromSidebar=this.auth.getRoleFromToken();
    this.userNameFromSidebar= this.auth.getuserNameFromToken();
    this.adminService.getMenuItem(this.userNameFromSidebar).subscribe(res=>{
      this.parentMenu=res.filter(x=>x.isParent===true).sort((a,b)=>a.DisplayOrder-b.DisplayOrder);
      this.menuList=res;
      console.log('parent',res);
    })
  }
  
  getChildren(parentId:number){
   this.childrenMenu= this.menuList.filter(r=>r.parentID===parentId).sort((a,b)=>a.DisplayOrder-b.DisplayOrder);
   console.log('child',this.childrenMenu);
  return this.childrenMenu;
  }

}

