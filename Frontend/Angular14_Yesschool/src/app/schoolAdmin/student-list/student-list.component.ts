import { Component, OnInit } from '@angular/core';
import { StudentAdmissionService } from 'src/app/services/student-admission.service';
import { IGetStudentInfoDto } from '../Dto/IGetStudentInfoDto';

@Component({
  selector: 'app-student-list',
  templateUrl: './student-list.component.html',
  styleUrls: ['./student-list.component.css']
})
export class StudentListComponent implements OnInit {

 studentInfoList:IGetStudentInfoDto[];
  constructor(private studentAdmissionService:StudentAdmissionService) {
    studentAdmissionService.getStudentList().subscribe((data)=>{
      this.studentInfoList=data;
    })
   }

  ngOnInit(): void {
  }

}
