import { FolderAction, FOLDER_ACTION } from '../actions/folder.action'; 

const initialState= {
    folders : [ 
        
    ],
    selectedFolders : [

    ],
    isInited:false
}

export default function folderReducer(state = initialState, action:FolderAction){
    
    switch(action.type){

        case FOLDER_ACTION.CLEAR_FOLDERS:{
            return {
                folders: [],   
                selectedFolders: [],
                isInited : false
            }
  
          }

        case FOLDER_ACTION.LOAD_FOLDERS:{

            return {
                folders: [...action.payload],   
                selectedFolders: [],
                isInited : true
            }
  
          }

          case FOLDER_ACTION.UPDATE_FOLDER:{
            return {
                ...state,
                folders: [...state.folders.map(i=>{ 
                    if(i.id === action.payload.id)  
                        i.title = action.payload.title;

                    return i;
                }) ],   
                selectedFolders: state.selectedFolders
            } 

        }

        case FOLDER_ACTION.ADD_FOLDER:{
          return {
             ...state,
            folders: [...state.folders, ...action.payload],
            selectedFolders: [...state.selectedFolders]
          }

        }

        case FOLDER_ACTION.PROCESS_SELECTION_FOLDER:{
            let idx = state.selectedFolders.indexOf(action.payload);
  
            if(idx>=0)
                return {
                    ...state,
                    folders: [...state.folders],
                    selectedFolders: [
                        ...state.selectedFolders.filter(i =>
                                i.id !== action.payload.id
                        )
                    ]
                }    
            else{ 
                  
                return {
                    ...state,
                    folders: [...state.folders],
                    selectedFolders: [...state.selectedFolders,action.payload]
                }
            }
        }

        case FOLDER_ACTION.DELETE_FOLDER_FOREVER:{
  
            return {
                ...state,
                folders: [...state.folders.filter(i=>{ 
                    if(i.id != action.payload.id)
                        return i;
                    
                }) ],   
                selectedFolders: []
            } 

        }

        case FOLDER_ACTION.RESTORE_FOLDER:{
  
            return {
                ...state,
                folders: [...state.folders.map(i=>{ 
                    if(i.id === action.payload.id) 
                        i.isDeleted = action.payload.isDeleted;

                    return i;
                }) ],   
                selectedFolders: []
            } 

        }

        case FOLDER_ACTION.DELETE_FOLDER:{
            return {
                ...state,
                folders: [...state.folders.map(i=>{

                    if(state.selectedFolders.find(s=>s.id === i.id)!==undefined)
                        i.isDeleted = true;
    
                    return i;
                }) ],   
                selectedFolders: []
            }
        }


        case FOLDER_ACTION.CLEAR_SELECTION_FOLDER:{
            return {
                ...state,
                files: [...state.folders ],   
                selectedFolders: []
            }
        }

        default: return state;
    }

}