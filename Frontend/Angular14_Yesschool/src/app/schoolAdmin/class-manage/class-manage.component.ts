import { Component, OnInit } from '@angular/core';
import { AdminMasterService } from 'src/app/services/admin-master.service';

@Component({
  selector: 'app-class-manage',
  templateUrl: './class-manage.component.html',
  styleUrls: ['./class-manage.component.css']
})
export class ClassManageComponent implements OnInit {

  constructor(private adminMasterService:AdminMasterService) { }

  ngOnInit(): void {
    this.adminMasterService.getClassList().subscribe((res)=>{
      console.log({res});
    })
  }

}
