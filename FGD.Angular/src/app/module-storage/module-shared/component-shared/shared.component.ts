import { Component, OnInit } from '@angular/core';

import { FileModel } from '../../shared/models/file.model';
import { FolderModel } from '../../shared/models/folder.model';

import { Observable } from 'rxjs';
import { Files } from 'src/app/redux/transfer-models/file.model.transfer';
import { FileState, FolderState } from 'src/app/redux/app.state';
import { Store } from '@ngrx/store';
import { Folders } from 'src/app/redux/transfer-models/folder.model.transfer'; 

import { SharedFileService } from '../../shared/api/shared-file.service';
import { SharedFolderService } from '../../shared/api/shared-folder.service';
import { FolderPageAction } from 'src/app/redux/actions/folder.action';
import { selectFolders } from 'src/app/redux/selectors/folder.selector';
import { FilePageAction } from 'src/app/redux/actions/file.action';
import { selectFiles } from 'src/app/redux/selectors/file.selector';

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

    this.filesState = this.store.select(selectFiles); 
    this.foldersState = this.folderStore.select(selectFolders);
  }

  onFileClicked(file:FileModel){

     this.store.dispatch(FilePageAction.process_selection_file(
          file
     ))
  }

    onFolderClicked(folder:FolderModel){ 

     this.store.dispatch(FolderPageAction.process_selection_folder(
          folder
     ))
  }


}
