import { AuthAction, AUTH_ACTION } from '../actions/auth.action';

const initialState= {
    userId : localStorage.getItem("userId"),
    token:  localStorage.getItem("token")
}

export default function authReducer(state = initialState, action:AuthAction){
    
    switch(action.type){

        case AUTH_ACTION.LOG_IN :{
 
            if(action.payload.token === null|| action.payload.token==="")
                return state;

                localStorage.setItem("token", action.payload.token); 
                localStorage.setItem("userId", action.payload.userId); 
   
          return {
             ...state,
             userId : action.payload.id,
             token : action.payload.token
          }

        }
 
        case AUTH_ACTION.LOG_OUT:{
            
            localStorage.removeItem("token");
            localStorage.removeItem("userId");

            return {
                ...state,
                token : "",
                userId : -1
             }
        }
  
        default: return state;
    }

}