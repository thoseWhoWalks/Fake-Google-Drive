import { Injectable } from '@angular/core';

import { FileModel } from "../models/file.model";
import { NotificationModel } from '../models/notification.model';

import { Store } from '@ngrx/store';
import { NotificationsState } from 'src/app/redux/app.state';
import { AddNotification, MarkAllAsRead, LoadNotifications } from 'src/app/redux/actions/notification.action';

import { HttpClientHelper } from 'src/app/shared/helper/http-client.helper';

@Injectable()
export class NotificationService {

    constructor(private httpClientHelper: HttpClientHelper,
        private NotificationStore: Store<NotificationsState>
    ) {
    }

    NOTIFICATION_API: string = `Notification/`;

    NOTIFICATION_GET_BY_USER_ID: string = `${this.NOTIFICATION_API}GetAllByUserId/`

    NOTIFICATION_CHANGE_STATE: string = `${this.NOTIFICATION_API}MarkAsRead/`

    public Load(id: number) {

        this.httpClientHelper.Get<NotificationModel[]>(this.NOTIFICATION_GET_BY_USER_ID + id)
            .subscribe(data => {

                if (data.ok)
                    this.NotificationStore.dispatch(new LoadNotifications(data.item));
                else
                    console.error(data.errors[0].message);
            });
    }

    public AddNotificationFromSignalR(notification: NotificationModel) {

        if (notification === undefined || notification === null)
            return;

        this.NotificationStore.dispatch(new AddNotification(notification));

    }

    public MarkAllAsRead(id: number) {

        this.httpClientHelper.Put<FileModel>(this.NOTIFICATION_CHANGE_STATE + id)
            .subscribe(data => {

                if (data.ok)
                    this.NotificationStore.dispatch(new MarkAllAsRead(data.item));
                else
                    console.error(data.errors[0].message);
            });
    }


}