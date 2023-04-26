import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { FolderModel } from '../../models/folder.model';
import { Store } from '@ngrx/store';
import { FolderState, NavigatorState, FileState } from 'src/app/redux/app.state';
import { Observable } from 'rxjs';
import { Folders } from 'src/app/redux/transfer-models/folder.model.transfer';
import { FolderService } from '../../api/folder.service';
import { FileService } from '../../api/file.service';
import { NavigatorPageActions } from 'src/app/redux/actions/navigator.actions';
import { FolderPageAction } from 'src/app/redux/actions/folder.action';
import { selectFolders } from 'src/app/redux/selectors/folder.selector';

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
    this.foldersState = this.folderStore.select(selectFolders);
  }

  foldersState: Observable<Folders>;

  public isSelected : boolean = false;

  @Input() folder : FolderModel;

  onFolderClicked(){ 

    this.isSelected = !this.isSelected;

    this.folderStore.dispatch(FolderPageAction.process_selection_folder(
         this.folder
    ))
 }

   onFolderDoubleClicked(){
     
    if(this.folder.isDeleted === true){
        alert("This folder is deleted! If u want to see it's content - restore it!");
        return;
    }

    this.navigator.dispatch(NavigatorPageActions.push_folder(
      this.folder
    ))

    this.folderService.GetByParentFolderId(this.folder.id);

    this.fileService.GetByParentFolderId(this.folder.id);

   }
}
