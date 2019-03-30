import { NgModule } from '@angular/core';
import { RouterModule} from "@angular/router";
import { CommonModule } from '@angular/common';

import { MaterialModule } from '../app-material.module';
import {ContentModule} from "./module-content/content.module";
import { ShareModule } from './module-shared/share.module';
import { BinModule } from "./module-bin/bin.module";

import { StorageComponent } from "./component-storage/storage.component";

import {MenuComponent } from "./shared/components/menu/component-menu/menu.component";
import {NavigationComponent} from './shared/components/menu/component-navigation/navigation.component';
import { StorageDetailsComponent } from './shared/components/menu/component-storage-details/storage-details.component';
import { ToolbarComponent } from './shared/components/toolbar/component-toolbar/toolbar.component';
import { OptionsBarComponent } from './shared/components/options-bar/options-bar.component'; 
import { NewButtonComponent } from './shared/components/menu/component-new-button/new-button.component'; 
import { CreateFolderMenuItem } from './shared/components/options-bar/create-folder-menu-item/create-new-folder-menu-item.component'; 
import { ShareMenuItem } from './shared/components/options-bar/share-menu-item/share-menu-item.component'; 
import { InfoMenuItem } from './shared/components/options-bar/info-menu-item/info-menu-item.component'; 

import { MoreOption } from './shared/components/options-bar/more-option-component/more-option.component'; 

@NgModule({
  declarations: [
    StorageComponent,
    MenuComponent,
    NavigationComponent,
    StorageDetailsComponent,
    ToolbarComponent, 
    OptionsBarComponent,
    NewButtonComponent,
    CreateFolderMenuItem,
    ShareMenuItem,
    InfoMenuItem,
    MoreOption
  ],
  imports: [
    CommonModule,
    ContentModule,
    ShareModule,
    BinModule,
    RouterModule,
    MaterialModule
  ],
  exports: [
    StorageComponent 
  ]
})
export class StorageModule { }
