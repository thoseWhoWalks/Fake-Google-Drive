import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { Store } from '@ngrx/store';
import { Router } from '@angular/router';
import { AuthState, NotificationsState, FolderState, FileState } from 'src/app/redux/app.state';
import { LogOut } from 'src/app/redux/actions/auth.action';
import { Observable, Subscription } from 'rxjs';
import { Notifications } from 'src/app/redux/transfer-models/notification.model.transfer';
import { MarkAllAsRead } from 'src/app/redux/actions/notification.action'; 
import { NotificationState } from '../../../types/notification-state.type';
import { MatMenuTrigger } from '@angular/material/menu';
import { NotificationService } from '../../../api/notification.service';
import { ClearFolders } from 'src/app/redux/actions/folder.action';
import { ClearFiles } from 'src/app/redux/actions/file.action';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.css']
})
export class ToolbarComponent implements OnInit, OnDestroy {

  constructor(
              private folderStore : Store<FolderState>,
              private fileStore : Store<FileState>,
              private authStore : Store<AuthState>,
              private notificationStore : Store<NotificationsState>,
              private notificationService : NotificationService,
              private router: Router) { }

  newNotificationsCount : number = 0;

  notifictionsState : Observable<Notifications>;

  notifications;

  @ViewChild(MatMenuTrigger) trigger: MatMenuTrigger;

  subscription : Subscription;

  ngOnInit() {

    this.notifictionsState = this.notificationStore.select(selectNotifications);

    this.subscription = this.authStore.select("authPage").subscribe(res => {
       
       this.notifications = res;

       this.notifictionsState.subscribe(notifications => 
          this.newNotificationsCount = notifications.notifications.filter(e => e.notificationState == NotificationState.New).length);

    });

    this.notificationService.Load(Number.parseInt(localStorage.getItem("userId")));
  }

  onExitClicked(){ 

    this.authStore.dispatch(AuthPageActions.log_out());

    this.folderStore.dispatch(new ClearFolders());

    this.fileStore.dispatch(new ClearFiles());

    this.router.navigateByUrl('/');
  }
 

  onNotificationsMenuClosed(){ 

    this.notifications.forEach(element => {
      if(element.notificationState == NotificationState.New){

        this.notificationService.MarkAllAsRead(element.id);
        return;
      }
    });

  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

}
