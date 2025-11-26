import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './guards/auth.guard';
import { RoleComponent } from './role/role.component';
import { MenuAccessComponent } from './menu-access/menu-access.component';
import { ClassManageComponent } from './schoolAdmin/class-manage/class-manage.component';
import { SectionManageComponent } from './schoolAdmin/section-manage/section-manage.component';
import { FeesNameComponent } from './schoolAdmin/fees-name/fees-name.component';
import { StudentAdmissionComponent } from './schoolAdmin/student-admission/student-admission.component';
import { StudentListComponent } from './schoolAdmin/student-list/student-list.component';

const routes: Routes = [
   {path:'', redirectTo:'login', pathMatch:'full'}
  ,{path:'login',component:LoginComponent}
  ,{path:'signup',component:SignupComponent,canActivate:[AuthGuard]}
  ,{path:'home',component:HomeComponent,canActivate:[AuthGuard]}
  ,{path:'role',component:RoleComponent,canActivate:[AuthGuard]}
  ,{path:'menu',component:MenuAccessComponent,canActivate:[AuthGuard]}
  ,{path:"class_list",component:ClassManageComponent,canActivate:[AuthGuard]}
  ,{path:"section_list",component:SectionManageComponent,canActivate:[AuthGuard]}
  ,{path:"FeesName_list",component:FeesNameComponent,canActivate:[AuthGuard]}
  ,{path:"Student_Admission",component:StudentAdmissionComponent,canActivate:[AuthGuard]}
  ,{path:"Student_Admission/:id/edit",component:StudentAdmissionComponent,canActivate:[AuthGuard]}
  ,{path:"Student_List",component:StudentListComponent,canActivate:[AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
