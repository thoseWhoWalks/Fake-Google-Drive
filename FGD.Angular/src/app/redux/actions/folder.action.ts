import { Action } from '@ngrx/store';
import { FolderModel } from 'src/app/module-storage/shared/models/folder.model'; 

export namespace FOLDER_ACTION{

    export const ADD_FOLDER = 'ADD_FOLDER'
    export const DELETE_FOLDER = 'DELETE_FOLDER' 

    export const UPDATE_FOLDER = 'UPDATE_FOLDER'

    export const PROCESS_SELECTION_FOLDER  = 'PROCESS_SELECTION_FOLDER'
    export const CLEAR_SELECTION_FOLDER = 'CLEAR_SELECTION_FOLDER'

    export const DELETE_FOLDER_FOREVER = 'DELETE_FOLDER_FOREVER';
    export const RESTORE_FOLDER = "RESTORE_FOLDER"; 

    export const LOAD_FOLDERS = 'LOAD_FOLDERS';

    export const CLEAR_FOLDERS = 'CLEAR_FOLDERS';
}

export class AddFolder implements Action{
    
    readonly type = FOLDER_ACTION.ADD_FOLDER;

    constructor(public payload:FolderModel) {  
    }

}

export class ClearFolders implements Action{
    
    readonly type = FOLDER_ACTION.CLEAR_FOLDERS;

    constructor(public payload=null) {  
    }

}

export class DeleteFolder implements Action{

    readonly type = FOLDER_ACTION.DELETE_FOLDER;

    constructor(public payload:FolderModel){ } 
}

export class UpdateFolder implements Action{

    readonly type = FOLDER_ACTION.UPDATE_FOLDER;

    constructor(public payload:FolderModel){ } 
}

export class ProcessSelectionFolder implements Action{

    readonly type = FOLDER_ACTION.PROCESS_SELECTION_FOLDER;

    constructor(public payload:FolderModel){ }
} 
 
export class DeleteFolderForever implements Action{

    readonly type: string = FOLDER_ACTION.DELETE_FOLDER_FOREVER;
    
    constructor(public payload ) { }

}

export class RestoreFolder implements Action{

    readonly type: string = FOLDER_ACTION.RESTORE_FOLDER;
    
    constructor(public payload ) { }

}

export class ClearSelectionFolder implements Action{

    readonly type: string = FOLDER_ACTION.CLEAR_SELECTION_FOLDER;
    
    constructor(public payload) { }
}

export class LoadFolders implements Action{

    readonly type: string = FOLDER_ACTION.LOAD_FOLDERS;
    
    constructor(public payload:FolderModel[]) { }

}

export type FolderAction = AddFolder | DeleteFolder |
            ProcessSelectionFolder | RestoreFolder | 
            DeleteFolderForever | ClearSelectionFolder | 
            LoadFolders | UpdateFolder |
            ClearFolders;