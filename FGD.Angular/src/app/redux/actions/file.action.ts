import { FileModel } from 'src/app/module-storage/shared/models/file.model';
import { Action } from '@ngrx/store';

export namespace FILE_ACTION{

    export const ADD_FILE = 'ADD_FILE'
    export const DELETE_FILE = 'DELETE_FILE'

    export const UPDATE_FILE = 'UPDAET_FILE'

    export const PROCESS_SELECTION_FILE  = 'PROCESS_SELECTION_FILE'
    export const CLEAR_SELECTION_FILE = 'CLEAR_SELECTION_FILE'

    export const DELETE_FILE_FOREVER = 'DELETE_FILE_FOREVER';
    export const RESTORE_FILE = "RESTORE_FILE";
      
    export const LOAD_FILES = 'LOAD_FILES';

    export const CLEAR_FILES = 'CLEAR_FILES';
}
 

export class DeleteFileForever implements Action{

    readonly type: string = FILE_ACTION.DELETE_FILE_FOREVER;
    
    constructor(public payload) { }

}

export class ClearFiles implements Action{

    readonly type: string = FILE_ACTION.CLEAR_FILES;
    
    constructor(public payload = null) { }

}

export class RestoreFile implements Action{

    readonly type: string = FILE_ACTION.RESTORE_FILE;
    
    constructor(public payload ) { }

}

export class AddFile implements Action{
    
    readonly type = FILE_ACTION.ADD_FILE;

    constructor(public payload:FileModel[]) {  }

}

export class DeleteFile implements Action{

    readonly type = FILE_ACTION.DELETE_FILE;

    constructor(public payload:FileModel){ } 

}

export class UpdateFile implements Action{

    readonly type = FILE_ACTION.UPDATE_FILE;

    constructor(public payload:FileModel){}

}

export class ProcessSelectionFile implements Action{

    readonly type = FILE_ACTION.PROCESS_SELECTION_FILE;

    constructor(public payload:FileModel){}
} 

export class ClearSelectionFile implements Action{

    readonly type: string = FILE_ACTION.CLEAR_SELECTION_FILE;
    
    constructor(public payload) { }
} 

export class LoadFiles implements Action{

    readonly type: string = FILE_ACTION.LOAD_FILES;
    
    constructor(public payload) { }

}

export type FileAction = AddFile | DeleteFile |
                 ProcessSelectionFile | ClearSelectionFile |
                 DeleteFileForever | RestoreFile |
                 LoadFiles | UpdateFile |
                 ClearFiles;