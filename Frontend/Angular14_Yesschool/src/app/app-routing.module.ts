import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './guards/auth.guard';
import { RoleComponent } from './role/role.component';
import { MenuAccessComponent } from './menu-access/menu-access.component';

const routes: Routes = [
   {path:'', redirectTo:'login', pathMatch:'full'}
  ,{path:'login',component:LoginComponent},
  ,{path:'signup',component:SignupComponent,canActivate:[AuthGuard]}
  ,{path:'home',component:HomeComponent,canActivate:[AuthGuard]}
  ,{path:'role',component:RoleComponent,canActivate:[AuthGuard]}
  ,{path:'menu',component:MenuAccessComponent,canActivate:[AuthGuard]}
  ,{path:"class_list",component:HomeComponent,canActivate:[AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
