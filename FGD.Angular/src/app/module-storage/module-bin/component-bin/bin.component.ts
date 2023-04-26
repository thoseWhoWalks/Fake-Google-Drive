import { Component, OnInit } from '@angular/core';

import { FileService } from '../../shared/api/file.service';
import { FolderService } from '../../shared/api/folder.service';

import { FileModel } from '../../shared/models/file.model';
import { FolderModel } from '../../shared/models/folder.model';

import { Observable } from 'rxjs';
import { Files } from 'src/app/redux/transfer-models/file.model.transfer';
import { FileState, FolderState } from 'src/app/redux/app.state';
import { Store } from '@ngrx/store';
import { Folders } from 'src/app/redux/transfer-models/folder.model.transfer'; 
import { FolderPageAction } from 'src/app/redux/actions/folder.action';
import { selectFolders } from 'src/app/redux/selectors/folder.selector';
import { FilePageAction } from 'src/app/redux/actions/file.action';
import { selectFiles } from 'src/app/redux/selectors/file.selector';

@Component({
  selector: 'app-bin',
  templateUrl: './bin.component.html',
  styleUrls: ['./bin.component.css']
})
export class BinComponent implements OnInit {

  constructor(
      private store: Store<FileState>,
      private folderStore : Store<FolderState>,
      private fileService : FileService,
      private folderService : FolderService
      ) { }

  foldersState: Observable<Folders>;
  filesState: Observable<Files>;

  ngOnInit() { 
    this.fileService.GetDeletedByUserId();
    this.folderService.GetDeletedByUserId();

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
