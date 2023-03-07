import { Injectable } from '@angular/core'; 

import { FileService } from '../api/file.service';
import { FolderService } from '../api/folder.service';
import { SharedFolderService } from '../api/shared-folder.service';
import { SharedFileService } from '../api/shared-file.service';

import { Store } from '@ngrx/store'; 
import { Clear, RollBack } from 'src/app/redux/actions/navigator.actions';
import { NavigatorState } from 'src/app/redux/app.state';

import { FolderModel } from '../models/folder.model';
 
@Injectable()
export class NavigatorService{

    navigatorState : Array<FolderModel>;

    constructor(
        private fileService : FileService,
        private folderService : FolderService,
        private sharedFolderService : SharedFolderService,
        private sharedFileService : SharedFileService,
        private navigatorStore: Store<NavigatorState>
        ){
            this.navigatorStore.select("stack").subscribe(({instance})=>{
                this.navigatorState = instance
            }) ;
        }

    public RollBackToRoot() {
        
        this.folderService.GetRootByUserId();
        this.fileService.GetRootByUserId();

        this.navigatorStore.dispatch(new Clear());
    }

    public ClearNavigationBreadCrumb() {
        this.navigatorStore.dispatch(new Clear());
    }

    public RollBackToSharedRoot() {

        this.sharedFolderService.GetByUserId();
        this.sharedFileService.GetByUserId();

        this.navigatorStore.dispatch(new Clear());
    }

    public RollBack(rollBackTo:FolderModel){

        this.folderService.GetByParentFolderId(rollBackTo.id);
        this.fileService.GetByParentFolderId(rollBackTo.id);

        this.navigatorStore.dispatch(new RollBack(rollBackTo.id));
    }

    public GetCurrentFolderIdOrNull():number{

         if(this.navigatorState.length > 0)
            return this.navigatorState[this.navigatorState.length-1].id 
         
        return null;
    }

}