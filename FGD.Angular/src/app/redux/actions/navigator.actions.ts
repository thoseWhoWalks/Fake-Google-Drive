import { Action } from '@ngrx/store';
import { FolderModel } from 'src/app/module-storage/shared/models/folder.model'; 

export namespace NAVIGATOR_ACTION{
    export const PUSH_FOLDER = 'PUSH_FOLDER'
    export const ROLL_BACK = 'ROLL_BACK'  
    export const CLEAR = 'CLEAR'
}

export class PushFolder implements Action{
    
    readonly type = NAVIGATOR_ACTION.PUSH_FOLDER;

    constructor(public payload:FolderModel) {  
    }

}

export class RollBack implements Action{

    readonly type = NAVIGATOR_ACTION.ROLL_BACK;

    constructor(public payload:number){
    
    }


} 

export class Clear implements Clear{

    readonly type = NAVIGATOR_ACTION.CLEAR;

    constructor(){
    
    }
}

export type NavigatorAction = PushFolder | RollBack | Clear ;