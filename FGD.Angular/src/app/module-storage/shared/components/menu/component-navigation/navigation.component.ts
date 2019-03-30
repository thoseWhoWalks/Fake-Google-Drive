import { Component, OnInit } from '@angular/core';
import { FolderState, FileState } from 'src/app/redux/app.state';
import { Store } from '@ngrx/store';
import { ClearSelectionFile, ClearFiles } from 'src/app/redux/actions/file.action';
import { ClearSelectionFolder, ClearFolders } from 'src/app/redux/actions/folder.action';
import { NavigatorService } from '../../../services/navigator.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {

  constructor(private folderStore : Store<FolderState>,
              private fileStore : Store<FileState>,
              private navigatorService : NavigatorService) { }

  ngOnInit() { }

  onLinkClicked(){

    this.navigatorService.ClearNavigationBreadCrumb();

    this.folderStore.dispatch(new ClearSelectionFile(null));

    this.fileStore.dispatch(new ClearSelectionFolder(null));

    this.folderStore.dispatch(new ClearFolders());

    this.fileStore.dispatch(new ClearFiles());
    
  }

}
