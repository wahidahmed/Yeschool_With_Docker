import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgToastService } from 'ng-angular-popup';
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
    this.adminMasterService.getClassList().subscribe((res)=>{
      console.log({res});
    })
  }

  onSubmit(){
    
  }
}
