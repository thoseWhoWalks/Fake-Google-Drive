import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { NgModule } from '@angular/core';

import { StorageModule } from "./module-storage/storage.module";
import { WelcomeModule } from "./module-welcome/welcome.module";

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { JwtModule } from '@auth0/angular-jwt';
import { JwtModuleOptions } from '@auth0/angular-jwt';

import { AuthGuardService } from './shared/services/auth-guard.service';
import { AuthService } from './module-account/shared/api/auth-service';
import { SubscriptionService } from './shared/api/subscription.service';
import { NotificationService } from './module-storage/shared/api/notification.service';
import { SignalRService } from './shared/api/signalR.service';

import { StoreModule } from '@ngrx/store';
import fileReducer from './redux/reducers/file.reducer';
import folderReducer from './redux/reducers/folder.reducer';
import navigatorReducer from './redux/reducers/navigator.reducer';
import authReducer from './redux/reducers/auth.reducer';
import subscriptionReducer from './redux/reducers/subscription.reducer';
import notificationReducer from './redux/reducers/notification.reducer';

import { StoreDevtoolsModule } from '@ngrx/store-devtools';

import { HttpClientHelper } from './shared/helper/http-client.helper';

export const JWT_Module_Options: JwtModuleOptions = {
  config: {
    tokenGetter: tokenGetter,
    whitelistedDomains: ['localhost:3001', 'foo.com', 'bar.com']
  }
};

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    StorageModule,
    WelcomeModule,
    MatProgressSpinnerModule,
    BrowserAnimationsModule,
    JwtModule.forRoot(JWT_Module_Options),
    StoreModule.forRoot({
      filePage: fileReducer,
      folderPage: folderReducer,
      navigator: navigatorReducer,
      authPage: authReducer,
      subscriptionPage: subscriptionReducer,
      notificationPage: notificationReducer
    }),
    StoreDevtoolsModule
  ],
  providers: [
    SubscriptionService,
    AuthGuardService,
    AuthService,
    NotificationService,
    SignalRService,
    HttpClientHelper
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

export function tokenGetter() {
  return localStorage.getItem("token");
}

