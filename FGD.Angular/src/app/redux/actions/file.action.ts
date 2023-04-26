import { FileModel } from 'src/app/module-storage/shared/models/file.model';
import { createActionGroup, props } from '@ngrx/store';

export const FileApiActions = createActionGroup({
    source: 'API',
    events: {
        'LOAD_FILES': props<{ files: FileModel[] }>(),
    },
});

export const FilePageAction = createActionGroup({
    source: 'Page',
    events: {
        'PROCESS_SELECTION_FILE': props<FileModel>(),
        'ADD_FILE': props<{ files: FileModel[] }>(),
        'UPDATE_FILE': props<FileModel>(),
        'DELETE_FILE': props<FileModel>(),
        'DELETE_FILE_FOREVER': props<FileModel>(),
        'RESTORE_FILE': props<FileModel>(),
    },
});

export const FileNavigationAction = createActionGroup({
    source: 'Navigation',
    events: {
        'CLEAR_FILES': props<any>(),
        'CLEAR_SELECTION_FILE': props<any>()
    },
});
