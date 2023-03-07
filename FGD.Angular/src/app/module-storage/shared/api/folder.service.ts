import { Injectable } from '@angular/core';

import { FolderModel } from '../models/folder.model';

import { Store } from '@ngrx/store';
import { FolderState, NavigatorState } from 'src/app/redux/app.state';
import { LoadFolders, DeleteFolder, AddFolder, UpdateFolder } from 'src/app/redux/actions/folder.action';

import { HttpClientHelper } from 'src/app/shared/helper/http-client.helper';


@Injectable()
export class FolderService {

    navigatorState: Array<FolderModel>;

    userId: number = 0;

    constructor(private httpClientHelper: HttpClientHelper,
        private folderStore: Store<FolderState>,
        private navigatorStore: Store<NavigatorState>
    ) {
        this.navigatorStore.select("stack").subscribe(({ instance }) => {
            this.navigatorState = instance
        });

        this.userId = Number.parseInt(localStorage.getItem("userId"));
    }

    STORED_FOLDER_API: string = `StoredFolder/`;

    FOLDER_GETBYROOT_API: string = `StoredFolder/GetByRootByUserId/`;
    FOLDER_GET_BY_PARENT_FOLDER_ID = `StoredFolder/GetByParentId/`;

    FILE_GET_DELETED: string = `${this.STORED_FOLDER_API}GetDeleted/`

    public GetRootByUserId() {

        this.httpClientHelper.Get<FolderModel[]>(this.FOLDER_GETBYROOT_API + this.userId).subscribe(data => {

            if (data.ok)
                this.folderStore.dispatch(new LoadFolders(data.item));
            else
                console.error(data.errors[0].message);
        });
    }

    public GetByParentFolderId(id: number) {

        this.httpClientHelper.Get<FolderModel[]>(this.FOLDER_GET_BY_PARENT_FOLDER_ID + id).subscribe(data => {

            if (data.ok)
                this.folderStore.dispatch(new LoadFolders(data.item));
            else
                console.error(data.errors[0].message);
        });
    }

    public GetDeletedByUserId() {

        this.httpClientHelper.Get<FolderModel[]>(this.FILE_GET_DELETED + this.userId).subscribe(data => {

            if (data.ok)
                this.folderStore.dispatch(new LoadFolders(data.item));
            else
                console.error(data.errors[0].message);
        });
    }

    public Update(folder: FolderModel) {

        if (folder.title === undefined)
            return

        this.httpClientHelper.Put<FolderModel>(this.STORED_FOLDER_API + folder.id, folder)
            .subscribe(data => {

                if (data.ok)
                    this.folderStore.dispatch(new UpdateFolder(data.item));
                else
                    console.error(data.errors[0].message);
            }
            );

    }

    public CreateFolder(title: string) {

        let folder = new FolderModel()
            .WithTitle(title)
            .WithStoredFolderId(this.GetCurrentFolderIdOrNull());

        this.httpClientHelper.Post<FolderModel>(this.STORED_FOLDER_API + this.userId, folder)
            .subscribe(data => {

                if (data.ok)
                    this.folderStore.dispatch(new AddFolder(data.item));
                else
                    console.error(data.errors[0].message);
            });

    }

    public DeleteById(id: any) {

        this.httpClientHelper.Delete<FolderModel>(this.STORED_FOLDER_API + id).subscribe(data => {

            if (data.ok)
                this.folderStore.dispatch(new DeleteFolder(data.item));
            else
                console.error(data.errors[0].message);
        });
    }

    public GetCurrentFolderIdOrNull(): number {
        if (this.navigatorState.length > 0)
            return this.navigatorState[this.navigatorState.length - 1].id

        return null;
    }

}