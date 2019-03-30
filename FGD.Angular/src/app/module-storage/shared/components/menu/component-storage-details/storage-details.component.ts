import { Component, OnInit, OnDestroy } from '@angular/core';
import { SubscriptionState } from 'src/app/redux/app.state';
import { Store } from '@ngrx/store';
import { Subscription } from 'src/app/redux/transfer-models/subscription.model.transfer';
import { SubscriptionService } from 'src/app/shared/api/subscription.service';

@Component({
  selector: 'app-storage-details',
  templateUrl: './storage-details.component.html',
  styleUrls: ['./storage-details.component.css']
})
export class StorageDetailsComponent implements OnInit, OnDestroy {
  
  constructor(private subscriptionStore : Store<SubscriptionState>,
    private subscriptionService : SubscriptionService) { }

  takenPlace : number;

  private subscription;

  subscriptionState : Subscription = null;

  ngOnInit() {
 
    this.subscription = this.subscriptionStore.select("subscriptionPage").subscribe(res => {
        this.subscriptionState = res; 
        
        if(res!=null)
          this.takenPlace = this.calculateTakenSpacePercentage(); 
    })

    this.subscriptionService.GetSubscriptionInformation();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  private calculateTakenSpacePercentage():number{
   
    return  (this.subscriptionState.subscription.takenSpace/1024/1024) * 100 / (this.subscriptionState.subscription.totalSpace);
  } 
  

}
