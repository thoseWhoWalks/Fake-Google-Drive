import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './component-header/header.component';
import { WelcomeComponent } from './component-welcome/welcome.component'; 
import {RouterModule} from '@angular/router';  

import { MaterialModule } from '../app-material.module';

@NgModule({
  declarations: [HeaderComponent, WelcomeComponent],
  imports: [
    CommonModule,
    RouterModule,
    MaterialModule
  ],
  exports: [
    WelcomeComponent
  ]
})
export class WelcomeModule { }
