import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MaterialModule} from '../app-material.module';
import {RouterModule} from '@angular/router';  
import {FormsModule} from '@angular/forms';
import { HttpClientModule } from '@angular/common/http'; 

import { SignInComponent } from './component-sign-in/sign-in.component';
import { SignUpComponent } from './component-sign-up/sign-up.component';
import { HeaderComponent } from './shared/components/component-header/header.component';

import { JwtHelperService } from '@auth0/angular-jwt';
import { AccountService } from './shared/api/account-service';

@NgModule({
  declarations: [
    SignInComponent, 
    HeaderComponent,  
    SignUpComponent,
  ],
  imports: [
    CommonModule,
    MaterialModule ,
    RouterModule, 
    FormsModule,
    HttpClientModule
  ],
  providers: [
    JwtHelperService,
    AccountService
  ],
  exports: [
    SignInComponent,
    SignUpComponent
  ]
})
export class AccountModule { }
