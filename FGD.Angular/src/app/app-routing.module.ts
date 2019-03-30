import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router'; 

import { AccountModule } from './module-account/account.module';
import {WelcomeModule} from './module-welcome/welcome.module';
import {WelcomeComponent} from './module-welcome/component-welcome/welcome.component';
import {StorageModule} from './module-storage/storage.module'; 

import {StorageComponent} from './module-storage/component-storage/storage.component';
import { ContentComponent } from './module-storage/module-content/component-content/content.component';
import { BinComponent } from './module-storage/module-bin/component-bin/bin.component';
import { SignInComponent } from './module-account/component-sign-in/sign-in.component';
import { SignUpComponent } from './module-account/component-sign-up/sign-up.component';
import { SharedComponent } from './module-storage/module-shared/component-shared/shared.component';

import { AuthGuardService } from './shared/services/auth-guard.service';

const routes: Routes = [
  {path:'signin', component: SignInComponent},
  {path:'signup',component: SignUpComponent}, 
  {path:'', component: WelcomeComponent,pathMatch: 'full'},
  {path:'storage', component: StorageComponent,canActivate: [AuthGuardService],children: [
    { path: '', component: ContentComponent, outlet: 'storage-outlet',pathMatch: 'full' },
    { path: 'shared', component: SharedComponent, outlet: 'storage-outlet' },
    { path: 'bin', component: BinComponent, outlet: 'storage-outlet' }
  ] } 
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes),
    WelcomeModule,
    StorageModule,
    AccountModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }
