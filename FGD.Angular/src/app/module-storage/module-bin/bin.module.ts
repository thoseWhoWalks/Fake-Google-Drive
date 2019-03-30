import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MaterialModule } from '../../app-material.module';
import { SharedModule } from '../shared/shared.module';

import { BinComponent } from './component-bin/bin.component';

import { FilterNotDeletedPipe } from '../shared/pipes/filter-not-deleted.pipe';

@NgModule({
  declarations: [
    BinComponent,
    FilterNotDeletedPipe, 
  ],
  imports: [
    CommonModule,
    MaterialModule,
    SharedModule
  ],
  exports: [
    BinComponent
  ]
})
export class BinModule { }
