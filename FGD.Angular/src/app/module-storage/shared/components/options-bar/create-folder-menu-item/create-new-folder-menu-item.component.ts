import { Component  } from '@angular/core';
import {  MatDialog } from '@angular/material';
import { FolderModel } from '../../../models/folder.model';
import { FolderService } from '../../../api/folder.service';
import { CreateFolderDialog } from './create-folder-dialog/create-folder-dialog.component';

@Component({
    selector: 'create-new-folder-menu-item',
    templateUrl: 'create-new-folder-menu-item.component.html',
    styleUrls: ['./create-new-folder-menu-item.component.css']
  })

  export class CreateFolderMenuItem {
  
    constructor(public dialog: MatDialog,
                private folderService : FolderService
                ) {} 
  
     onCreateNewFolderClicked(){ 
 
        const dialogRef = this.dialog.open(CreateFolderDialog, {
          width: '250px',
          data: {folderProto:new FolderModel()}
        });
    
        dialogRef.afterClosed().subscribe(result => { 
          console.log(result);
          this.folderService.CreateFolder(result);
        }); 
      
    }
  }

   