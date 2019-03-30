
import { FolderModel } from 'src/app/module-storage/shared/models/folder.model';
import { NavigatorAction, NAVIGATOR_ACTION } from '../actions/navigator.actions';

export interface INavigationStack{
     instance : Array<FolderModel>
}

class Stack implements INavigationStack {

    constructor() {
        
    }

    instance:Array<FolderModel> = new Array<FolderModel>();

    public push(item:FolderModel):Stack{
        
        this.instance.push(item); 

        return this;
    }

    public rollBack(id:number):Stack{

        let idx = this.instance.findIndex(
            i => i.id === id
        )

        if(idx < 0)
            return this;

        this.instance.splice(idx+1);

        return this;
    }

    public clear():Stack{

        this.instance = [];

        return this;
    }

    public getInstance(){
        return this.instance;
    }

}

const initialState= {
     stack : new Stack()
}

export default function navigatorReducer(state = initialState, action:NavigatorAction){
    
    switch(action.type){

        case NAVIGATOR_ACTION.PUSH_FOLDER:{ 

          return {
             ...state,
            stack: state.stack.push(action.payload)
          }

        }

        case NAVIGATOR_ACTION.ROLL_BACK:{
           
                return {
                    ...state,
                   stack: state.stack.rollBack(action.payload)
                }
            
        }

        case NAVIGATOR_ACTION.CLEAR:{

            return {
                ...state,
               stack: state.stack.clear()
            }

        }

        default: return state;
    }

}