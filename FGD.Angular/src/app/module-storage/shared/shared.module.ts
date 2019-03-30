import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MaterialModule } from '../../app-material.module';

import { StoredFileComponent } from '../shared/components/stored-file/stored-file.component';
import { StoredFolderComponent } from '../shared/components/stored-folder/stored-folder.component';
import { CreateFolderDialog } from './components/options-bar/create-folder-menu-item/create-folder-dialog/create-folder-dialog.component'; 
import { RenameDialog } from './components/options-bar/more-option-component/rename-item-dialog/rename-dialog.component'; 
import { ShareDialog } from './components/options-bar/share-menu-item/share-item-dialog/share-dialog.component';
import { InfoBottomSheet } from './components/options-bar/info-menu-item/info-bottom-sheet/info-bottom-sheet.component';

import { FolderService } from './api/folder.service';
import { FileService } from './api/file.service';
import { BinService } from './api/bin.service';
import { NavigatorService } from './services/navigator.service';
import { SharedFileService } from './api/shared-file.service';
import { SharedFolderService } from './api/shared-folder.service';

import { FilterDeletedPipe } from '../shared/pipes/filter-deleted.pipe';

@NgModule({
  imports: [
    CommonModule,
    MaterialModule,
  ],
  providers:[
    FolderService,
    FileService,
    BinService,
    NavigatorService,
    SharedFileService,
    SharedFolderService
  ],
  declarations: [
    StoredFileComponent,
    StoredFolderComponent,
    CreateFolderDialog,
    RenameDialog,
    ShareDialog,
    FilterDeletedPipe,
    InfoBottomSheet
  ],
  exports: [
    StoredFileComponent,
    StoredFolderComponent,
    CreateFolderDialog,
    RenameDialog,
    ShareDialog,
    InfoBottomSheet,
    FilterDeletedPipe,
  ],
  entryComponents: [
    CreateFolderDialog,
    RenameDialog,
    ShareDialog,
    InfoBottomSheet
  ]
})

export class SharedModule { }