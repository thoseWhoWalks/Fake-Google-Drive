import { Injectable } from '@angular/core'; 

import { Store } from '@ngrx/store';
import { SubscriptionState } from 'src/app/redux/app.state';

import { LoadSubscriptionInfo, UpdateFromSignalR } from 'src/app/redux/actions/subscription.action';

import { HttpClientHelper } from '../helper/http-client.helper';

import SubscriptionModel from '../model/subscription/subscription.model';
import AccountSubscriptionModel from '../model/subscription/account-subscription.model';
import SubscriptionInfoModel from '../model/subscription/subscription-info.model';

@Injectable()
export class SubscriptionService{
  
    AccountSubscriptionBaseUrl : string = "AccountSubscription/";
 
    SubscriptionBaseUrl : string = "Subscription/";

    constructor(private httpClientHelper:HttpClientHelper,
                private subscriptionStore : Store<SubscriptionState>
                ) { }
    
    subInfoModel : SubscriptionInfoModel = new SubscriptionInfoModel();

    public GetSubscriptionInformation() {
      
        let id = localStorage.getItem("userId");

       this.httpClientHelper.Get<AccountSubscriptionModel>((
           this.AccountSubscriptionBaseUrl + 'GetByUserId/' + id))
           .subscribe(res => {
 
               this.subInfoModel.takenSpace = res.item.takenSpace;

               this.httpClientHelper.Get<SubscriptionModel>(this.SubscriptionBaseUrl  + res.item.subscriptionId)
                   .subscribe(sub => {
                      
                    this.subInfoModel.title = sub.item.title;
                    this.subInfoModel.totalSpace = sub.item.totalSpace;
 
                    this.subscriptionStore.dispatch(new LoadSubscriptionInfo(
                        this.subInfoModel
                    ))
                });

           });

    }

    public UpdateStateFromSignalR(updatedState : {takenSpace:number,isActive:boolean}){

        this.subscriptionStore.dispatch(new UpdateFromSignalR(updatedState))

    }

}
