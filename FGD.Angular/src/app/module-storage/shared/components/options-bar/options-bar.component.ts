import { Component, OnInit, OnDestroy } from '@angular/core';

import { Store } from '@ngrx/store';
import { FileState, FolderState, NavigatorState } from 'src/app/redux/app.state';

import { FolderModel } from '../../models/folder.model';
import { FileModel } from '../../models/file.model';

import { Router, NavigationEnd } from '@angular/router';

import { filter } from 'rxjs/operators';

import { FolderService } from '../../api/folder.service';
import { FileService } from '../../api/file.service';
import { BinService } from '../../api/bin.service';
import { NavigatorService } from '../../services/navigator.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-options-bar',
  templateUrl: './options-bar.component.html',
  styleUrls: ['./options-bar.component.css']
})
export class OptionsBarComponent implements OnInit, OnDestroy {


  constructor(private fileStore: Store<FileState>,
    private folderSotre: Store<FolderState>,
    private folderService: FolderService,
    private fileService: FileService,
    private binService: BinService,
    private router: Router,
    private navigatorStore: Store<NavigatorState>,
    private navigatorService: NavigatorService
  ) { }

  public selectedFiles: FileModel[] = [];
  public selectedFolders: FolderModel[] = [];

  navigatorState: NavigatorState;

  folderSubscription: Subscription = null;
  fileSubscription: Subscription = null;
  navigatorSubscription: Subscription = null;

  ngOnInit() {

    this.fileSubscription = this.fileStore.select("filePage").subscribe(({ selectedFiles }) => {
      this.selectedFiles = selectedFiles;
    });

    this.folderSubscription = this.folderSotre.select("folderPage").subscribe(({ selectedFolders }) => {
      this.selectedFolders = selectedFolders
    });

    this.navigatorSubscription = this.navigatorStore.select("stack").subscribe(res => {
      this.navigatorState.stack = res;
    });

    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe((event: NavigationEnd) => {
      this.parseUrl(event.url);
    });

  }

  title: string = "Storage";

  location: Panels = Panels.Storage;

  panels: any = Panels;

  private onDeleteClicked() {

    this.selectedFiles.forEach(i =>
      this.fileService.DeleteById(i.id)
    );

    this.selectedFolders.forEach(i => {
      this.folderService.DeleteById(i.id)
    });
  }

  private onDeleteFolderClicked() {

    this.folderService.DeleteById(
      this.navigatorService.GetCurrentFolderIdOrNull()
    );
  }

  private onDownloadClicked() {

    this.selectedFiles.forEach(i =>
      this.fileService.Download(i)
    );
  }

  private onRestoreClicked() {

    this.selectedFiles.forEach(i =>
      this.binService.RestoreFileById(i.id)
    );

    this.selectedFolders.forEach(i => {
      this.binService.RestoreFolderById(i.id)
    });
  }

  private onDeleteForeverClicked() {

    this.selectedFiles.forEach(i =>
      this.binService.DeleteFileForeverById(i.id)
    );

    this.selectedFolders.forEach(i => {
      this.binService.DeleteFolderForeverById(i.id)
    });
  }

  private onRollBackClicked(rollBackTo) {
    this.navigatorService.RollBack(rollBackTo);
  }

  private parseUrl(url: string) {

    if (url.includes('bin')) {
      this.title = "Bin";
      this.location = this.panels.Bin;
    }
    else if (url.includes('shared')) {
      this.title = "Shared";
      this.location = this.panels.Shared;
    }
    else {
      this.title = "Content";
      this.location = this.panels.Storage;
    }
  }

  onRollBackToRootClicked() {

    if (this.location == this.panels.Storage)
      this.navigatorService.RollBackToRoot();

    if (this.location == this.panels.Shared)
      this.navigatorService.RollBackToSharedRoot();

  }

  ngOnDestroy(): void {

    if (this.folderSubscription != null)
      this.folderSubscription.unsubscribe();

    if (this.fileSubscription != null)
      this.folderSubscription.unsubscribe();

    if (this.navigatorSubscription != null)
      this.folderSubscription.unsubscribe();
  }

}

enum Panels {
  Bin,
  Storage,
  Shared
}
