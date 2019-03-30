import { FolderModel } from 'src/app/module-storage/shared/models/folder.model';

export interface Folders{
    folders:FolderModel[],
    selectedFolders:FolderModel[],
    isInited:boolean
}