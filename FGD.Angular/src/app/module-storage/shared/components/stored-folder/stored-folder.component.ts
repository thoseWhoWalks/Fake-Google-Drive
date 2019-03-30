import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { FolderModel } from '../../models/folder.model';
import { ProcessSelectionFolder } from 'src/app/redux/actions/folder.action';
import { Store } from '@ngrx/store';
import { FolderState, NavigatorState, FileState } from 'src/app/redux/app.state';
import { Observable } from 'rxjs';
import { Folders } from 'src/app/redux/transfer-models/folder.model.transfer';
import { PushFolder } from 'src/app/redux/actions/navigator.actions';
import { FolderService } from '../../api/folder.service';
import { FileService } from '../../api/file.service';

@Component({
  selector: 'app-stored-folder',
  templateUrl: './stored-folder.component.html',
  styleUrls: ['./stored-folder.component.css']
})
export class StoredFolderComponent implements OnInit {

  constructor(private folderStore : Store<FolderState>,
              private fileStore : Store<FileState>,
              private folderService : FolderService,
              private fileService : FileService,
              private navigator : Store<NavigatorState>) {  
                
               }

  ngOnInit() {
    this.foldersState = this.folderStore.select("folderPage");
  }

  foldersState: Observable<Folders>;

  public isSelected : boolean = false;

  @Input() folder : FolderModel;

  onFolderClicked(){ 

    this.isSelected = !this.isSelected;

    this.folderStore.dispatch(new ProcessSelectionFolder(
         this.folder
    ))
 }

   onFolderDoubleClicked(){
     
    if(this.folder.isDeleted === true){
        alert("This folder is deleted! If u want to see it's content - restore it!");
        return;
    }

    this.navigator.dispatch(new PushFolder(
      this.folder
    ))

    this.folderService.GetByParentFolderId(this.folder.id);

    this.fileService.GetByParentFolderId(this.folder.id);

   }
}
