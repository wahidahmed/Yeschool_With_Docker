import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgToastService } from 'ng-angular-popup';
import ValidateForm from 'src/app/helpers/ValidateForm';
import { AdminMasterService } from 'src/app/services/admin-master.service';

@Component({
  selector: 'app-fees-name',
  templateUrl: './fees-name.component.html',
  styleUrls: ['./fees-name.component.css']
})
export class FeesNameComponent implements OnInit {

  
    constructor(private adminMasterService:AdminMasterService,private fb:FormBuilder, private toast:NgToastService) { }
    public feesNameForm:FormGroup;
    feesNameList:any[];
    ngOnInit(): void {
      this.feesNameForm=this.fb.group({
        feesNameId:[0,],
        feesName:[null,[Validators.required]],
        feesType:["",[Validators.required]],
        remarks:[null]
      })
      this.getList();
    }
  getList(){
      this.adminMasterService.getFeesNameList().subscribe(data=>{this.feesNameList=data; console.log(data)});
    }
    onSubmit(){
     
       if(this.feesNameForm.valid){
              this.adminMasterService.addFeesName(this.feesNameForm.controls['feesName'].value,this.feesNameForm.controls['feesType'].value,this.feesNameForm.controls['remarks'].value).subscribe({
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
            ValidateForm.validateAllFormFields(this.feesNameForm);
          }
    }

}
