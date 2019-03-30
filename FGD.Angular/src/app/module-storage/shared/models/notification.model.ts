import { NotificationState } from '../types/notification-state.type';

export class NotificationModel{

    constructor() { }

    public WithId(id:number):NotificationModel{
        this.id = id;
        return this;
    }

   public WithTitle(title:string):NotificationModel{
        this.title = title
        return this;
    }

    public WithState(state:NotificationState):NotificationModel{
        this.notificationState = state;
        return this;
    }

    id:number;

    title : string;

    notificationState : NotificationState

}