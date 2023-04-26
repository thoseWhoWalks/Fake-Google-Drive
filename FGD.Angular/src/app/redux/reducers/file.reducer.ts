import { createReducer, on } from '@ngrx/store';
import { FileApiActions, FilePageAction, FileNavigationAction } from '../actions/file.action';
import { FileState } from '../app.state';

const initialState = {
    files: [],
    selectedFiles: []
}

export const fileReducer = createReducer(
    initialState,
    on(FileApiActions.load_files, (state: FileState, { files }) => ({
        files: [...files],
        selectedFiles: [],
    })),

    on(FilePageAction.process_selection_file, (state: FileState, file) => {
        let idx = state.selectedFiles.indexOf(file);

        if (idx >= 0)
            return {
                ...state,
                files: [...state.files],
                selectedFiles: [
                    ...state.selectedFiles.filter(i =>
                        i.id !== file.id
                    )
                ]
            }
        else {

            return {
                ...state,
                files: [...state.files],
                selectedFiles: [...state.selectedFiles, file]
            }
        }
    }),

    on(FilePageAction.add_file, (state: FileState, file) => ({
        ...state,
        files: [...state.files, file],
        selectedFiles: [...state.selectedFiles]
    })),

    on(FilePageAction.update_file, (state: FileState, file) => ({
        ...state,
        files: [...state.files.map(i => {
            if (i.id === file.id)
                i.title = file.title;

            return i;
        })],
        selectedFiles: state.selectedFiles
    })),

    on(FilePageAction.delete_file, (state: FileState, file) => ({
        ...state,
        files: [...state.files.map(i => {

            if (state.selectedFiles.find(s => s.id === i.id) !== undefined)
                i.isDeleted = true;

            return i;
        })],
        selectedFiles: []
    })),

    on(FilePageAction.delete_file_forever, (state: FileState, file) => ({
        ...state,
        files: [...state.files.filter(i => {
            if (i.id != file.id)
                return i;

        })],
        selectedFiles: []
    })),

    on(FilePageAction.restore_file, (state: FileState, file) => ({
        ...state,
        files: [...state.files.map(i => {
            if (i.id === file.id)
                i.isDeleted = file.isDeleted;

            return i;
        })],
        selectedFiles: []
    })),

    on(FileNavigationAction.clear_files, (state: FileState) => ({
        ...state,
        files: [],
        selectedFiles: [],
    })),

    on(FileNavigationAction.clear_selection_file, (state: FileState) => ({
        ...state,
        files: [...state.files],
        selectedFiles: []
    })),
)