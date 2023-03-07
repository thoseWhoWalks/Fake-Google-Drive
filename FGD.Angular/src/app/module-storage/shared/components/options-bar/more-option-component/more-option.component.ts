import { Component, OnInit, OnDestroy } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { FolderService } from '../../../api/folder.service';
import { FileService } from '../../../api/file.service';

import { Store } from '@ngrx/store';
import { FolderState, FileState } from 'src/app/redux/app.state';

import { FileModel } from '../../../models/file.model';
import { FolderModel } from '../../../models/folder.model';

import { RenameDialog } from './rename-item-dialog/rename-dialog.component';
import { Subscription } from 'rxjs';

@Component({
  selector: 'more-option',
  templateUrl: 'more-option.component.html',
  styleUrls: ['./more-option.component.css']
})

export class MoreOption implements OnInit, OnDestroy {

  ngOnInit(): void {
    this.selectedFilesSubscription = this.fileStore.select("filePage").subscribe(({ selectedFiles }) => {
      this.selectedFiles = selectedFiles;
    });

    this.selectedFolderSubscription = this.folderSotre.select("folderPage").subscribe(({ selectedFolders }) => {
      this.selectedFolders = selectedFolders
    });
  }

  public selectedFiles: FileModel[] = [];
  public selectedFolders: FolderModel[] = [];

  selectedFilesSubscription: Subscription = null;
  selectedFolderSubscription: Subscription = null;

  constructor(
    private folderService: FolderService,
    private fileService: FileService,
    private fileStore: Store<FileState>,
    private folderSotre: Store<FolderState>,
    public dialog: MatDialog
  ) { }

  private onRenameClicked() {

    if (this.selectedFolders.length > 0)
      this.renameFolder(this.selectedFolders[0])

    if (this.selectedFiles.length > 0)
      this.renameFile(this.selectedFiles[0])
  }

  private renameFile(file: FileModel) {

    const dialogRef = this.dialog.open(RenameDialog, {
      width: '350px',
      data: { name: file.title }
    });

    dialogRef.afterClosed().subscribe(result => {

      file.title = result;

      this.fileService.Update(file);

    });
  }

  private renameFolder(folder: FolderModel) {

    const dialogRef = this.dialog.open(RenameDialog, {
      width: '350px',
      data: { name: folder.title }
    });

    dialogRef.afterClosed().subscribe(result => {

      folder.title = result;

      this.folderService.Update(folder);

    });
  }

  ngOnDestroy(): void {
    if (this.selectedFilesSubscription !== null)
      this.selectedFilesSubscription.unsubscribe();

    if (this.selectedFolderSubscription !== null)
      this.selectedFolderSubscription.unsubscribe();
  }

}

