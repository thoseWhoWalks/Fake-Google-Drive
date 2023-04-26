import { createActionGroup, props } from '@ngrx/store';
import { FolderModel } from 'src/app/module-storage/shared/models/folder.model';

export const FolderApiActions = createActionGroup({
    source: 'API',
    events: {
        'LOAD_FOLDERS': props<{ folders: FolderModel[] }>(),
    },
});

export const FolderPageAction = createActionGroup({
    source: 'Page',
    events: {
        'PROCESS_SELECTION_FOLDER': props<FolderModel>(),
        'ADD_FOLDER': props<FolderModel>(),
        'UPDATE_FOLDER': props<FolderModel>(),
        'DELETE_FOLDER': props<FolderModel>(),
        'DELETE_FOLDER_FOREVER': props<FolderModel>(),
        'RESTORE_FOLDER': props<FolderModel>(),
    },
});

export const FolderNavigationAction = createActionGroup({
    source: 'Navigation',
    events: {
        'CLEAR_FOLDERS': props<any>(),
        'CLEAR_SELECTION_FOLDER': props<any>()
    },
});
