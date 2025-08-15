import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AdminitrationService } from '../services/adminitration.service';
import { NgToastService } from 'ng-angular-popup';
import { UserCredStoreService } from '../services/user-cred-store.service';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-menu-access',
  templateUrl: './menu-access.component.html',
  styleUrls: ['./menu-access.component.css']
})
export class MenuAccessComponent implements OnInit {

  constructor(
    private fb:FormBuilder
    ,private adminstrationService:AdminitrationService
    , private toast:NgToastService
    ,private userStore:UserCredStoreService
    ,private auth:AuthService
    ) { }
  menuList:any[];
  parentMenus: any[] = [];
  childMenus: any[] = [];
  
  saveForm:FormGroup;
  public role:string;
  ngOnInit(): void {
   this.userStore.getRoleFromStore().subscribe(val=>{
      let rl=this.auth.getRoleFromToken();
      this.role=val|| rl;
      this.getMenus(this.role);
    })

    this.saveForm=this.fb.group({
      roleName:[this.role,[Validators.required]],
       menuIds: this.fb.array([])
    })


  }

  getMenus(role:string){
    this.adminstrationService.getMenusByRole(role).subscribe(data=>{
      console.log(data);
      this.menuList=data;
      this.parentMenus = this.menuList.filter(x => x.isParent);
      this.childMenus = this.menuList.filter(x => !x.isParent);
      data.forEach(e => {
        if(e.roleName==role){
          this.menuIds.push(this.fb.control(e.appContentId));
        }
      });
    })
  }


  get menuIds(): FormArray {
    return this.saveForm.get('menuIds') as FormArray;
  }

 
  onParentChange(event: Event, parentId: number) {
    const checked = (event.target as HTMLInputElement).checked;

    if (checked) {
      // Add parent
      if (!this.menuIds.value.includes(parentId)) {
        this.menuIds.push(this.fb.control(parentId));
      }
      // Enable & add all children
      const children = this.getChildren(parentId);
      console.log('children',children);
      children.forEach(child => {
        if (!this.menuIds.value.includes(child.appContentId)) {
          this.menuIds.push(this.fb.control(child.appContentId));
        }
      });
    } else {
      // Remove parent
      this.removeFromFormArray(parentId);
      // Remove children
      const children = this.getChildren(parentId);
      children.forEach(child => this.removeFromFormArray(child.appContentId));
    }
}

  onChildChange(event: Event, childId: number) {
    const checked = (event.target as HTMLInputElement).checked;

      if (checked) {
        if (!this.menuIds.value.includes(childId)) {
          this.menuIds.push(this.fb.control(childId));
        }
      } else {
        this.removeFromFormArray(childId);
      }
  }

   removeFromFormArray(id: number) {
    const index = this.menuIds.value.indexOf(id);
    if (index !== -1) {
      this.menuIds.removeAt(index);
    }
  }

  isChecked(id: number): boolean {
    return this.menuIds.value.includes(id);
  }


  getChildren(parentId:any){
    return this.childMenus.filter(x=>x.parentID==parentId);
  }

  onSubmit(){
     console.log(this.saveForm.value);
  }
}
