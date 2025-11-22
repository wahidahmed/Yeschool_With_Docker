import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AdminitrationService } from '../services/administration.service';
import { NgToastService } from 'ng-angular-popup';
import { UserCredStoreService } from '../services/user-cred-store.service';
import { AuthService } from '../services/auth.service';
import { IAssignAccess } from '../models/iAssignAccess.model';
import ValidateForm from '../helpers/ValidateForm';
import { ActivatedRoute } from '@angular/router';

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
    ,private activateRoute:ActivatedRoute
    ) { }
  menuList:any[];
  parentMenus: any[] = [];
  childMenus: any[] = [];
  checkedList:any[]=[];
  
  saveForm:FormGroup;
  public role:string;
  ngOnInit(): void {
    this.getDefaultInfo();
    this.saveForm=this.fb.group({
      roleName:[this.role,[Validators.required]],
       menuIds: this.fb.array([])
    })
  }

  getDefaultInfo(){
    const roleName = this.activateRoute.snapshot.queryParamMap.get('roleName');
      this.role=roleName;
     this.getMenus(this.role);
  }

  getMenus(role:string){
    this.adminstrationService.GetAppContentByRole(role).subscribe(data=>{;
      this.menuList=data;
      this.parentMenus = this.menuList.filter(x => x.isParent);
      this.childMenus = this.menuList.filter(x => !x.isParent);
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
    if(this.saveForm.valid){
      
      const dto:IAssignAccess={
        RoleName:this.saveForm.value.roleName,
        MenuIds:this.saveForm.value.menuIds
      }
      this.adminstrationService.assignAccess(dto).subscribe({
        next:(res)=>{
          this.saveForm.reset();
            window.location.reload();
            // this.getDefaultInfo();
           this.toast.success({detail:"SUCCESS", summary:res, duration: 5000});
        },
        error:(err)=>{
          this.toast.error({detail:"ERROR", summary:"Something when wrong!", duration: 5000});
        }
      })
    }
    else{
      ValidateForm.validateAllFormFields(this.saveForm);
    }
  }
}
