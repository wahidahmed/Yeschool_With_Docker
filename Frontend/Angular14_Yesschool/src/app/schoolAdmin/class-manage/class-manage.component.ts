import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgToastService } from 'ng-angular-popup';
import ValidateForm from 'src/app/helpers/ValidateForm';
import { AdminMasterService } from 'src/app/services/admin-master.service';

@Component({
  selector: 'app-class-manage',
  templateUrl: './class-manage.component.html',
  styleUrls: ['./class-manage.component.css']
})
export class ClassManageComponent implements OnInit {

  constructor(private adminMasterService:AdminMasterService,private fb:FormBuilder, private toast:NgToastService) { }
  public classForm:FormGroup;
  classList:any[];
  ngOnInit(): void {
    this.classForm=this.fb.group({
      classId:[0,],
      className:[null,[Validators.required]],
      remarks:[null]
    })
    this.getList();
  }
getList(){
    this.adminMasterService.getClassList().subscribe(data=>{this.classList=data; console.log(data)});
  }
  onSubmit(){
   
     if(this.classForm.valid){
            this.adminMasterService.addNewClass(this.classForm.controls['className'].value,this.classForm.controls['remarks'].value).subscribe({
              next:(res=>{
                this.getList();
                this.toast.success({detail:res,summary:"successfully added",duration:5000});
              }),
              error:(err=>{
                // console.log(err);
                 this.toast.error({detail:err.error,summary:'failed added',duration:5000});
              })
            })
           
        }
        else{
          ValidateForm.validateAllFormFields(this.classForm);
        }
  }
}
