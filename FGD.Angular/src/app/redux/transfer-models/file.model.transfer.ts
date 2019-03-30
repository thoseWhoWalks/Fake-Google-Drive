import { FileModel } from 'src/app/module-storage/shared/models/file.model';

export interface Files{
    files:FileModel[],
    selectedFiles:FileModel[]
}