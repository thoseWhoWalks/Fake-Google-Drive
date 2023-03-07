import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
    selector: 'share-dialog',
    templateUrl: 'share-dialog.component.html',
  })

  export class ShareDialog {
  
    constructor(
      public dialogRef: MatDialogRef<ShareDialog>,
      @Inject(MAT_DIALOG_DATA) public data: ShareDialogData) {

          data.email = "Enter Email"    

      }
  
    onNoClick(): void {
      this.dialogRef.close();
    }
  
  }

  export interface ShareDialogData {
    email : string
  }