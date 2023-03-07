import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FolderModel } from '../../../../models/folder.model';

@Component({
    selector: 'create-folder-dialog',
    templateUrl: 'create-folder-dialog.component.html',
  })

  export class CreateFolderDialog {
  
    constructor(
      public dialogRef: MatDialogRef<CreateFolderDialog>,
      @Inject(MAT_DIALOG_DATA) public data: DialogData) {

          data.folderProto.title = "Unnamed Folder"    

      }
  
    onNoClick(): void {
      this.dialogRef.close();
    }
  
  }

  export interface DialogData {
    folderProto : FolderModel
  }