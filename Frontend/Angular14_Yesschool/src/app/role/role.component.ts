import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AdminitrationService } from '../services/adminitration.service';
import ValidateForm from '../helpers/ValidateForm';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.css']
})
export class RoleComponent implements OnInit {

  constructor(private fb:FormBuilder,private adminstrationService:AdminitrationService, private toast:NgToastService) { }
  public roleForm:FormGroup;
  roleList:any[];
  ngOnInit(): void {
    this.roleForm=this.fb.group({
      roleId:new FormControl(0),
      roleName:['',[Validators.required,Validators.maxLength(255)]]
    })

    this.getList();
  }

  getList(){
    this.adminstrationService.getRoles().subscribe(data=>{this.roleList=data});
    console.log(this.roleList)
  }

  onSubmit(){
    if(this.roleForm.valid){
        this.adminstrationService.addNnewRole(this.roleForm.controls['roleName'].value).subscribe({
          next:(res=>{
              this.toast.success({detail:res.message,summary:'successfully added',duration:5000});
          }),
          error:(err=>{
            // alert(err?.error)
            console.log(err);
             this.toast.error({detail:err,summary:'successfully added',duration:5000});
          })
        })
       
    }
    else{
      ValidateForm.validateAllFormFields(this.roleForm);
    }
  }

}
