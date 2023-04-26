import { OnInit, Component, OnDestroy } from '@angular/core';

import { FolderState, FileState } from 'src/app/redux/app.state';
import { Store } from '@ngrx/store';
import { Subscription } from 'rxjs';

import { FileModel } from '../../../../models/file.model';
import { FolderModel } from '../../../../models/folder.model';
import { selectFolders } from 'src/app/redux/selectors/folder.selector';
import { selectFiles } from 'src/app/redux/selectors/file.selector';

@Component({
    selector: 'info-bottom-sheet',
    templateUrl: './info-bottom-sheet.component.html',
    styleUrls: ['./info-bottom-sheet.component.css']
})
export class InfoBottomSheet implements OnInit, OnDestroy {

    constructor(
        private fileStore: Store<FileState>,
        private folderSotre: Store<FolderState>
    ) { }

    ngOnInit() {

        this.subscriptionSelectedFiles = this.fileStore.select(selectFiles).subscribe(({ selectedFiles }) => {
            this.selectedFiles = selectedFiles;
        });

        this.subscriptionSelectedFolders = this.folderSotre.select(selectFolders).subscribe(({ selectedFolders }) => {
            this.selectedFolders = selectedFolders
        });

        if (this.selectedFiles.length > 0)
            this.currentItem = this.selectedFiles[0];
        
        if (this.selectedFolders.length > 0){
            this.currentItem = this.selectedFolders[0];
        } 
    }

    currentItem : any = null;
    sharingsOfCurrentItem = null;

    private subscriptionSelectedFiles: Subscription = null;
    private subscriptionSelectedFolders: Subscription = null;

    private selectedFiles: FileModel[] = [];
    private selectedFolders: FolderModel[] = [];

    ngOnDestroy(): void {

        if (this.subscriptionSelectedFiles != null)
            this.subscriptionSelectedFiles.unsubscribe();

        if (this.subscriptionSelectedFolders != null)
            this.subscriptionSelectedFolders.unsubscribe();

    }
}