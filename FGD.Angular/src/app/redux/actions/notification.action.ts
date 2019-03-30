import { Action } from '@ngrx/store';
import { NotificationModel } from 'src/app/module-storage/shared/models/notification.model';

export namespace NOTIFICATION_ACTION{

    export const LOAD_NOTIFICATIONS = 'LOAD_NOTIFICATIONS'

    export const MARK_ALL_AS_READ = 'MARK_ALL_AS_READ' 

    export const ADD_NOTIFICATION = 'ADD_NOTIFICATION'
 
}

export class LoadNotifications implements Action{
    
    readonly type : string = NOTIFICATION_ACTION.LOAD_NOTIFICATIONS;

    constructor(public payload:NotificationModel[]) {  
    }

} 

export class MarkAllAsRead implements Action{
    
    readonly type : string = NOTIFICATION_ACTION.MARK_ALL_AS_READ;

    constructor(public payload = null) {  
    }

} 

export class AddNotification implements Action{
    
    readonly type : string = NOTIFICATION_ACTION.ADD_NOTIFICATION;

    constructor(public payload = null) {  
    }

} 

export type NotificationAction = MarkAllAsRead|AddNotification|LoadNotifications;