import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
    selector: 'rename-dialog',
    templateUrl: 'rename-dialog.component.html',
  })

  export class RenameDialog {
  
    constructor(
      public dialogRef: MatDialogRef<RenameDialog>,
      @Inject(MAT_DIALOG_DATA) public data: RenameDialogData) {}
  
    onNoClick(): void {
      this.dialogRef.close();
    }
  
  }

  export interface RenameDialogData {
    name : string
  }