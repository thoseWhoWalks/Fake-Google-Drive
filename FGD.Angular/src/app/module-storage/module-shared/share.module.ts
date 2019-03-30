import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MaterialModule } from '../../app-material.module';
import { SharedModule } from '../shared/shared.module';

import { SharedComponent } from './component-shared/shared.component';

@NgModule({
  declarations: [
    SharedComponent,
  ],
  imports: [
    CommonModule,
    MaterialModule, 
    SharedModule,
  ],
  exports: [
    SharedComponent
  ]
})
export class ShareModule { }
