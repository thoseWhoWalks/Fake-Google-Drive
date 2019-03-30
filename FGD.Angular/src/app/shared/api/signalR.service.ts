import { Injectable } from '@angular/core';

import * as signalR from '@aspnet/signalr';

import { NotificationService } from 'src/app/module-storage/shared/api/notification.service';

import { AuthService } from 'src/app/module-account/shared/api/auth-service';
import { SubscriptionService } from './subscription.service';

import { NotificationModel } from 'src/app/module-storage/shared/models/notification.model';

@Injectable()
export class SignalRService {

  private SignalRConnectionString: string = "http://fakegoogledrive.eastus.cloudapp.azure.com:5001/notify?UserId=";

  private notifySignal: string = "notify";

  private updateSubscriptionSignal: string = "updateSubscription";

  private hubConnection: signalR.HubConnection;

  constructor(
    private notificationService: NotificationService,
    private subscriptionService: SubscriptionService,
    private authService: AuthService
  ) { }

  public Start() {

    let userId = localStorage.getItem("userId");

    if (userId === undefined || !this.authService.IsAuthenticated()) {
      console.warn("Signal r connection aborted, user is not in system")
      return;
    }

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.SignalRConnectionString + userId)
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.error('Error while starting connection: ' + err))

    this.hubConnection.on(this.notifySignal, (data: NotificationModel) => {
      this.notificationService.AddNotificationFromSignalR(data)
    });

    this.hubConnection.on(this.updateSubscriptionSignal, (data) => {
      this.subscriptionService.UpdateStateFromSignalR(data);
    });

  }

  public Stop() {
    this.hubConnection.stop();
  }

}
