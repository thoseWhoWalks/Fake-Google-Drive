import { createReducer, on } from "@ngrx/store"
import { FolderApiActions, FolderNavigationAction, FolderPageAction } from "../actions/folder.action"
import { FolderState } from "../app.state"

const initialState = {
    folders: [],
    selectedFolders: [],
    isInited: false
}

export const folderReducer = createReducer(
    initialState,
    on(FolderApiActions.load_folders, (state: FolderState, { folders }) => ({
        ...state,
        folders: [...folders],
        selectedFolders: [],
        isInited: true
    })),

    on(FolderPageAction.process_selection_folder, (state: FolderState, folder) => {
        let idx = state.selectedFolders.indexOf(folder);

        if (idx >= 0)
            return {
                ...state,
                folders: [...state.folders],
                selectedFolders: [
                    ...state.selectedFolders.filter(i =>
                        i.id !== folder.id
                    )
                ]
            }
        else {

            return {
                ...state,
                folders: [...state.folders],
                selectedFolders: [...state.selectedFolders, folder]
            }
        }
    }),

    on(FolderPageAction.add_folder, (state: FolderState, folder) => ({
        ...state,
        folders: [...state.folders, folder],
        selectedFolders: [...state.selectedFolders]
    })),

    on(FolderPageAction.update_folder, (state: FolderState, folder) => ({
        ...state,
        folders: [...state.folders.map(i => {
            if (i.id === folder.id)
                i.title = folder.title;

            return i;
        })],
        selectedFolders: state.selectedFolders
    })),

    on(FolderPageAction.delete_folder, (state: FolderState, folder) => ({
        ...state,
        folders: [...state.folders.map(i => {

            if (state.selectedFolders.find(s => s.id === i.id) !== undefined)
                i.isDeleted = true;

            return i;
        })],
        selectedFolders: []
    })),

    on(FolderPageAction.delete_folder_forever, (state: FolderState, folder) => ({
        ...state,
        folders: [...state.folders.filter(i => {
            if (i.id != folder.id)
                return i;

        })],
        selectedFolders: []
    })),

    on(FolderPageAction.restore_folder, (state: FolderState, folder) => ({
        ...state,
        folders: [...state.folders.map(i => {
            if (i.id === folder.id)
                i.isDeleted = folder.isDeleted;

            return i;
        })],
        selectedFolders: []
    })),

    on(FolderNavigationAction.clear_folders, (state: FolderState) => ({
        ...state,
        folders: [],
        selectedFolders: [],
        isInited: false
    })),

    on(FolderNavigationAction.clear_selection_folder, (state: FolderState) => ({
        ...state,
        folders: [...state.folders],
        selectedFolders: []
    })),
)
