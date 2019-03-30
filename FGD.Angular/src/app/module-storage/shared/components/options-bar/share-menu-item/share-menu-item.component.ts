import { Component, OnInit, OnDestroy } from '@angular/core';

import { MatDialog } from '@angular/material';
import { ShareDialog } from './share-item-dialog/share-dialog.component';

import { Store } from '@ngrx/store';
import { FileState, FolderState } from 'src/app/redux/app.state';
import { Subscription } from 'rxjs';

import { SharedFolderModel } from '../../../models/shared-folder.model';
import { SharedFileModel } from '../../../models/shared-file.model';
import { FolderModel } from '../../../models/folder.model';
import { FileModel } from '../../../models/file.model';

import { SharedFolderService } from '../../../api/shared-folder.service';
import { SharedFileService } from '../../../api/shared-file.service';

@Component({
  selector: 'share-menu-item',
  templateUrl: 'share-menu-item.component.html',
})
export class ShareMenuItem implements OnInit, OnDestroy {


  ngOnInit(): void {
    this.selectedFilesSubscription = this.fileStore.select("filePage").subscribe(({ selectedFiles }) => {
      this.selectedFiles = selectedFiles;
    });

    this.selectedFoldersSubscription = this.folderStore.select("folderPage").subscribe(({ selectedFolders }) => {
      this.selectedFolders = selectedFolders;
    });
  }

  private selectedFilesSubscription: Subscription = null;

  private selectedFoldersSubscription: Subscription = null;

  private dialogSubscription: Subscription = null;

  private selectedFiles: FileModel[] = [];

  private selectedFolders: FolderModel[] = [];

  constructor(public dialog: MatDialog,
    private sharedFileService: SharedFileService,
    private sharedFolderService: SharedFolderService,
    private fileStore: Store<FileState>,
    private folderStore: Store<FolderState>,
  ) { }

  onShareClicked() {

    const dialogRef = this.dialog.open(ShareDialog, {
      width: '250px',
      data: { email: "" }
    });

    this.dialogSubscription = dialogRef.afterClosed().subscribe(result => {

      this.shareSelectedFiles(result);

      this.shareSelectedFolders(result);

    });

  }

  private shareSelectedFolders(result: any) {
    this.selectedFolders.forEach(f => {
      let model = new SharedFolderModel()
        .WithStoredFolderId(f.id)
        .WithAccountEmail(result);
      this.sharedFolderService.Create(model);
    });
  }

  private shareSelectedFiles(result: any) {
    this.selectedFiles.forEach(f => {
      let model = new SharedFileModel()
        .WithStoredFileId(f.id)
        .WithAccountEmail(result);
      this.sharedFileService.Create(model);
    });
  }

  ngOnDestroy(): void {
    if (this.selectedFilesSubscription !== null)
      this.selectedFilesSubscription.unsubscribe();

    if (this.selectedFoldersSubscription !== null)
      this.selectedFoldersSubscription.unsubscribe();

    if (this.dialogSubscription !== null)
      this.dialogSubscription.unsubscribe();
  }
}

