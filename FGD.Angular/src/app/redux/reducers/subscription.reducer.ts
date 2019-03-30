import { SubscriptionAction, SUBSCRIPTION_ACTION } from '../actions/subscription.action';
import SubscriptionModel from 'src/app/shared/model/subscription/subscription-info.model';
import SubscriptionInfoModel from 'src/app/shared/model/subscription/subscription-info.model';

var sub = new SubscriptionInfoModel();

const initialState = {
    subscription: sub
}

export default function subscriptionReducer(state = initialState, action: SubscriptionAction) {

    switch (action.type) {

        case SUBSCRIPTION_ACTION.LOAD_SUBSCRIPTION_INFO: {
            return {
                ...state,
                subscription: action.payload
            }
        }

        case SUBSCRIPTION_ACTION.UPDATE_SUBSCRIPTION_FROM_SIGNALR: {

            let sub = new SubscriptionInfoModel()
                .WithId(state.subscription.id)
                .WithTitle(state.subscription.title)
                .WithTotlaSpace(state.subscription.totalSpace)
                .WithTakenSpace(action.payload.takenSpace)

            return {
                ...state,
                subscription: sub
            }
        }

        default: return state;
    }

}