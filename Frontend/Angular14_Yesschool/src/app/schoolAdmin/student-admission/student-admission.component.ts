import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import ValidateForm from 'src/app/helpers/ValidateForm';
import { StudentAdmissionService } from 'src/app/services/student-admission.service';
import { ActivatedRoute } from '@angular/router';
import { IGetStudentInfoDto } from '../Dto/IGetStudentInfoDto';

@Component({
  selector: 'app-student-admission',
  templateUrl: './student-admission.component.html',
  styleUrls: ['./student-admission.component.css']
})
export class StudentAdmissionComponent implements OnInit {

  studentInfoEdit:IGetStudentInfoDto;
  constructor(private studentAdmitService:StudentAdmissionService
    ,private fb:FormBuilder
    ,private route:ActivatedRoute) { }

  admitFormGroup:FormGroup;
  ngOnInit(): void {
    this.admitForm();
    this.route.paramMap.subscribe(params=>{
      const id=params.get('id');
        if(id){
          this.studentAdmitService.getStudentList().subscribe((data)=>{
            this.studentInfoEdit=data[0];
            console.log(this.studentInfoEdit);
            this.admitFormGroup.patchValue({
              studentInfo:{
                
              }
            })
          })
        }
    })

  }

  admitForm(){
    this.admitFormGroup=this.fb.group({
      studentInfo:this.fb.group({
        StudentId:[0],
        AcademicYearId:['',[Validators.required]],
        GuardianName:[null,[Validators.required]],
        GuardianMobileNo:[null,[Validators.required]],
        GuardianRelation:['',[Validators.required]],
        Status:['PENDING',[Validators.required]],
        ClassesId:['',[Validators.required]],
        PersonalnfoId:[0],
        PersonName:[null,[Validators.required]],
        Mobile:[null],
        Email:[null],
        DateOfBirth:[null,[Validators.required]],
        FatherName:[null,[Validators.required]],
        FatherMobile:[null],
        FatherOccupation:[null],
        MotherName:[null,[Validators.required]],
        MotherMobile:[null],
        MotherOccupation:[null],
        Gender:['',[Validators.required]],
        Religion:['',[Validators.required]],
        ImageUrl:[null],
        PersonCode:[null]
      }),
      presentAddress:this.fb.group({
        AddressId:[0],
        DistrictId:['',[Validators.required]],
        ThanaId:[''],
        StreetDetail:[null],
        AddressType:['PRESENT',[Validators.required]],
      }),
      permanentAddress:this.fb.group({
        AddressId:[0],
        DistrictId:['',[Validators.required]],
        ThanaId:[''],
        StreetDetail:[null],
        AddressType:['PERMANENT',[Validators.required]],
      })
    })
  }

  bindEditData(){
    
  }
  formErrors={
    'PersonName':'',
    'Gender':'',
    'Religion':'',
    'DateOfBirth':'',
    'FatherName':'',
    'MotherName':'',
    'fatherMobile':'',
    'motherMobile':'',
    'AcademicYearId':'',
    'ClassesId':'',
    'GuardianName':'',
    'GuardianRelation':'',
    'GuardianMobileNo':'',
    'DistrictId':'',
    'AddressType':'',
    'presentDistrictId':'',
    'presentThanaId':'',
    'presentType':'',
    'permanentDistrictId':'',
    'permanentThanaId':'',
    'permanentType':'',
  }

  validationMessages={
    'PersonName':{
      'required':'Student Name is required',
      'maxlength':'Student Name cannot greater than 500 charecter'
    },
    'Gender':{
      'required':'gender is required'
    },
    'Religion':{
      'required':'religion is required'
    },
    'DateOfBirth':{
      'required':'dateOfBirth is required'
    },
    'FatherName':{
      'required':'fatherName is required'
    },
    'MotherName':{
      'required':'motherName is required'
    },
    'fatherMobile':{
      'maxlength':'Mobile number max length 11'
    },
    'motherMobile':{
      'maxlength':'Mobile number max length 11'
    },
    'AcademicYearId':{
      'required':'AcademicYear is required'
    },
    'ClassesId':{
      'required':'Class is required'
    },
    'GuardianName':{
      'required':'Guardian Name is required',
      'maxlength':'Guardian Name cannot greater than 500 charecter'
    },
    'GuardianRelation':{
      'required':'Guardian Relation is required'
    },
    'GuardianMobileNo':{
      'required':'Guardian contact number is required',
      'maxlength':'contact number cannot greater than 11 charecter'
    },
    'DistrictId':{
      'required':'Distrcit is required'
    },
    'AddressType':{
      'required':'Type is required',
    },
    'presentDistrictId':{
      'required':'Present District  is required'
    },
    'presentThanaId':{
      'required':'Present Thana  is required'
    },
    'presentType':{
      'required':'Address Type  is required'
    },
    'permanentDistrictId':{
      'required':'Permanent District  is required'
    },
    'permanentThanaId':{
      'required':'Permanent Thana  is required'
    },
    'permanentType':{
      'required':'Address Type  is required'
    }
  }

  onBlur(){
      ValidateForm.logValidationErrors(this.admitFormGroup,this.formErrors,this.validationMessages);
  }

  onSubmit(){
    console.log(this.admitFormGroup);
  }
}
