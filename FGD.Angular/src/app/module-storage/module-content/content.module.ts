import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import {MaterialModule} from '../../app-material.module';
import { SharedModule } from '../shared/shared.module';

import { ContentComponent } from './component-content/content.component';   

import {FileService} from '../shared/api/file.service';
import {FolderService} from '../shared/api/folder.service';
import { BinService } from '../shared/api/bin.service';

import {FileDropDirective} from './directives/file-drop.directive';

@NgModule({
  declarations: [
    ContentComponent, 
    FileDropDirective,
  ],
  providers:
  [
    FileService,
    FolderService,
    BinService
  ],
  imports: [
    CommonModule,
    MaterialModule,
    SharedModule
  ]
})
export class ContentModule { }
