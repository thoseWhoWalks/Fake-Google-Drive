import { NotificationState } from 'src/app/module-storage/shared/types/notification-state.type';
import { NOTIFICATION_ACTION, NotificationAction } from '../actions/notification.action';

const initialState= {
     notifications : [
         
     ]
}

export default function notificationReducer(state = initialState, action:NotificationAction){
    
    switch(action.type){

        case NOTIFICATION_ACTION.LOAD_NOTIFICATIONS:{
            return {
                ...state,
                notifications : [...action.payload]
             }

        }

        case NOTIFICATION_ACTION.MARK_ALL_AS_READ:{

          return {
             ...state,
             notifications : state.notifications.map( i => {
                 i.notificationState = NotificationState.Read

                 return i;
             })  
          }

        }

        case NOTIFICATION_ACTION.ADD_NOTIFICATION:{
            return {
               ...state,
               notifications : [...state.notifications,action.payload]
            }
  
          }
  
        default: return state;
    }

}