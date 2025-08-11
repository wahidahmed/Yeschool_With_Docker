import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { HeaderComponent } from './layout/header/header.component';
import { SidebarComponent } from './layout/sidebar/sidebar.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { HomeComponent } from './home/home.component';
import { NgToastModule } from 'ng-angular-popup';
import { TokenInterceptor } from './interceptors/token.interceptor';
import { RoleComponent } from './role/role.component';
import { MaterialModuleModule } from './material.module.module';
import { MenuAccessComponent } from './menu-access/menu-access.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    SignupComponent,
    HeaderComponent,
    SidebarComponent,
    HomeComponent,
    RoleComponent,
    MenuAccessComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgToastModule,
    MaterialModuleModule,
  ],
  providers: [
    {
    provide:HTTP_INTERCEPTORS,
    useClass:TokenInterceptor,
    multi:true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
