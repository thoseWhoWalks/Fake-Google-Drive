import { Component, OnInit } from '@angular/core';

import { FileModel } from '../../shared/models/file.model';
import { FolderModel } from '../../shared/models/folder.model';

import { Observable } from 'rxjs';
import { Files } from 'src/app/redux/transfer-models/file.model.transfer';
import { FileState, FolderState } from 'src/app/redux/app.state';
import { Store } from '@ngrx/store';
import { Folders } from 'src/app/redux/transfer-models/folder.model.transfer'; 
import { ProcessSelectionFile } from 'src/app/redux/actions/file.action';
import { ProcessSelectionFolder } from 'src/app/redux/actions/folder.action';

import { SharedFileService } from '../../shared/api/shared-file.service';
import { SharedFolderService } from '../../shared/api/shared-folder.service';

@Component({
  selector: 'app-shared',
  templateUrl: './shared.component.html',
  styleUrls: ['./shared.component.css']
})
export class SharedComponent implements OnInit {

  constructor(
      private store: Store<FileState>,
      private folderStore : Store<FolderState>,
      private sharedFileService : SharedFileService,
      private sharedFolderService : SharedFolderService
      ) { }

  foldersState: Observable<Folders>;
  filesState: Observable<Files>;

  ngOnInit() { 
    this.sharedFileService.GetByUserId();
    this.sharedFolderService.GetByUserId();

    this.filesState = this.store.select("filePage"); 
    this.foldersState = this.folderStore.select("folderPage");
  }

  onFileClicked(file:FileModel){

     this.store.dispatch(new ProcessSelectionFile(
          file
     ))
  }

    onFolderClicked(folder:FolderModel){ 

     this.store.dispatch(new ProcessSelectionFolder(
          folder
     ))
  }


}
