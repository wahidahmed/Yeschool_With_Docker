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
  checkedList:any[]=[];
  
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
    this.adminstrationService.GetAppContentByRole(role).subscribe(data=>{;
      this.menuList=data;
      this.parentMenus = this.menuList.filter(x => x.isParent);
      this.childMenus = this.menuList.filter(x => !x.isParent);
      console.log('childmenus',this.childMenus);
      data.forEach(e => {
        this.checkedList.push(e.appContentId);
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
      if(!this.checkedList.includes(parentId)){
        this.checkedList.push(parentId);
      }
      // Enable & add all children
      const children = this.getChildren(parentId);
      children.forEach(child => {
        if (!this.menuIds.value.includes(child.appContentId) && child.roleName==this.role) {
          this.menuIds.push(this.fb.control(child.appContentId));
        }
        if(!this.checkedList.includes(child.appContentId)){
          this.checkedList.push(child.appContentId);
        }
       
      });
    } else {
      // Remove parent
      this.removeFromFormArray(parentId);
      // Remove children
      const children = this.getChildren(parentId);
      // children.forEach(child => this.removeFromFormArray(child.appContentId));
      children.forEach((child)=>{
        this.removeFromFormArray(child.appContentId);
      })
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
    const inddex1=this.checkedList.indexOf(id);
    if (index !== -1) {
      this.menuIds.removeAt(index);
    }
    if(inddex1!==-1){
      this.checkedList.splice(inddex1, 1);
    }
  }

  isChecked(id: number): boolean {
   return this.menuIds.value.includes(id);
  }
 isEnabled(id: number): boolean {
    let parentId=this.getParent(id)??0;
    return this.checkedList.includes(parentId); //&& this.checkedList.includes(id) ;
  }

  getChildren(parentId:any){
    return this.childMenus.filter(x=>x.parentID==parentId);
  }

  getParent(childId:any){
    let parent=this.childMenus.find(x=>x.appContentId==childId && x.parentID!=0 && x.isParent==false);
    return parent?.parentID;
  }


  onSubmit(){
     console.log(this.saveForm.value);
  }
}
