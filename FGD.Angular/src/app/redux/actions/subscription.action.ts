import { Action } from '@ngrx/store';
import SubscriptionInfoModel from 'src/app/shared/model/subscription/subscription-info.model';
import { SubscriptionUpdateModel } from 'src/app/shared/model/subscription/subscription-update.model';

export namespace SUBSCRIPTION_ACTION{

    export const LOAD_SUBSCRIPTION_INFO = 'LOAD_SUBSCRIPTION_INFO'

    export const UPDATE_SUBSCRIPTION_FROM_SIGNALR = 'UPDATE_SUBSCRIPTION_FROM_SIGNALR'

}
  
export class UpdateFromSignalR implements Action{

    readonly type: string = SUBSCRIPTION_ACTION.UPDATE_SUBSCRIPTION_FROM_SIGNALR;
    
    constructor(public payload:SubscriptionUpdateModel) { }

}

export class LoadSubscriptionInfo  implements Action{

    readonly type: string = SUBSCRIPTION_ACTION.LOAD_SUBSCRIPTION_INFO;
    
    constructor(public payload : SubscriptionInfoModel) { }

}

export type SubscriptionAction = LoadSubscriptionInfo;