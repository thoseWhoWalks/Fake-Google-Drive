import { Component, OnInit } from '@angular/core';

import { FolderService } from '../../shared/api/folder.service';
import { FileService } from '../../shared/api/file.service';
import { NavigatorService } from '../../shared/services/navigator.service';

import { FileModel } from '../../shared/models/file.model';

import { Store } from '@ngrx/store';
import { FileState, FolderState } from 'src/app/redux/app.state';
import { Observable } from 'rxjs';
import { Files } from 'src/app/redux/transfer-models/file.model.transfer';
import { Folders } from 'src/app/redux/transfer-models/folder.model.transfer';
import { selectFolders } from 'src/app/redux/selectors/folder.selector';
import { selectFiles } from 'src/app/redux/selectors/file.selector';

@Component({
  selector: 'app-content',
  templateUrl: './content.component.html',
  styleUrls: ['./content.component.css']
})
export class ContentComponent implements OnInit {

  constructor(private store: Store<FileState>,
    private folderStore: Store<FolderState>,
    private folderService: FolderService,
    private fileService: FileService,
    private navigatorService: NavigatorService
  ) {
    let folderId = this.navigatorService.GetCurrentFolderIdOrNull();

    if (folderId !== null) {
      this.folderService.GetByParentFolderId(folderId);
      this.fileService.GetByParentFolderId(folderId);
      return;
    }

    this.folderService.GetRootByUserId();
    this.fileService.GetRootByUserId();
  }

  public filesState: Observable<Files>;
  public foldersState: Observable<Folders>;

  dropZoneActive: boolean;

  ngOnInit() {
    this.filesState = this.store.select(selectFiles);
    this.foldersState = this.folderStore.select(selectFolders);

    this.folderStore.select(selectFolders).subscribe(res => console.log(res));
  }

  handleDrop(files: FileList) {

    for (let i = 0; i < files.length; i++) {

      this.fileService.Upload(
        new FileModel()
          .WithFile(files[i])
          .WithStoredFolderId(
            this.navigatorService.GetCurrentFolderIdOrNull()
          ));
    }

  }

  triggerDropZoneState($event: boolean) {
    this.dropZoneActive = $event;
  }

}
