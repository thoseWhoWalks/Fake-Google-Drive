import { FILE_ACTION, FileAction } from '../actions/file.action';
 
const initialState= {
    files : [ 
    ],

    selectedFiles : [

    ]
}

export default function fileReducer(state = initialState, action:FileAction){
    
    switch(action.type){

        case FILE_ACTION.CLEAR_FILES:{
  
            return {
                files: [],   
                selectedFiles: []
            }
  
          }

        case FILE_ACTION.LOAD_FILES:{
  
            return {
                files: [...action.payload],   
                selectedFiles: []
            }
  
          }

        case FILE_ACTION.ADD_FILE:{
          return {
             ...state,
            files: [...state.files, ...action.payload],
            selectedFiles: [...state.selectedFiles]
          }

        }

        case FILE_ACTION.PROCESS_SELECTION_FILE:{

            let idx = state.selectedFiles.indexOf(action.payload);
  
            if(idx>=0)
                return {
                    ...state,
                    files: [...state.files],
                    selectedFiles: [
                        ...state.selectedFiles.filter(i =>
                                i.id !== action.payload.id
                        )
                    ]
                }    
            else{ 

                return {
                    ...state,
                    files: [...state.files],
                    selectedFiles: [...state.selectedFiles,action.payload]
                }
            }
        }

        case FILE_ACTION.DELETE_FILE:{
            return {
                ...state,
                files: [...state.files.map(i=>{

                    if(state.selectedFiles.find(s=>s.id === i.id)!==undefined)
                        i.isDeleted = true;
    
                    return i;
                }) ],   
                selectedFiles: []
            }
        }

        case FILE_ACTION.DELETE_FILE_FOREVER:{

            return {
                ...state,
                files: [...state.files.filter(i=>{ 
                    if(i.id != action.payload.id) 
                        return i;
                    
                }) ],   
                selectedFiles: []
            } 

        }

        case FILE_ACTION.RESTORE_FILE:{
  
            return {
                ...state,
                files: [...state.files.map(i=>{ 
                    if(i.id === action.payload.id)
                        i.isDeleted = action.payload.isDeleted;
                    
                    return i;
                }) ],   
                selectedFiles: []
            } 

        }

        case FILE_ACTION.UPDATE_FILE:{

            return {
                ...state,
                files: [...state.files.map(i=>{ 
                    if(i.id === action.payload.id)
                        i.title = action.payload.title;

                    return i;
                }) ],   
                selectedFiles: state.selectedFiles
            } 

        }

        case FILE_ACTION.CLEAR_SELECTION_FILE:{
            return {
                ...state,
                files: [...state.files ],   
                selectedFiles: []
            }
        }

        default: return state;
    }

}