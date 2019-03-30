import { Injectable } from '@angular/core';

import { FileModel } from "../models/file.model";

import { Store } from '@ngrx/store';
import { FileState, FolderState } from 'src/app/redux/app.state';
import { RestoreFile, DeleteFileForever } from 'src/app/redux/actions/file.action';
import { DeleteFolderForever, RestoreFolder } from 'src/app/redux/actions/folder.action';

import { HttpClientHelper } from 'src/app/shared/helper/http-client.helper';

@Injectable()
export class BinService {

    constructor(private httpClientHelper: HttpClientHelper,
        private fileStore: Store<FileState>,
        private folderStore: Store<FolderState>,
    ) {
    }

    BIN_API: string = `Bin/`;

    BIN_RESTORE_FILE_BY_ID_API: string = `${this.BIN_API}RecoverFileById/`;
    BIN_DELETE_FILE_FOREVER_BY_ID_API: string = `${this.BIN_API}DeleteFileForeverById/`;

    BIN_RESTORE_FOLDER_BY_ID_API: string = `${this.BIN_API}RecoverFolderById/`;
    BIN_DELETE_FOLDER_FOREVER_BY_ID_API: string = `${this.BIN_API}DeleteFolderForeverById/`;

    public RestoreFileById(id: number) {

        this.httpClientHelper.Get<FileModel>(this.BIN_RESTORE_FILE_BY_ID_API + id).subscribe(data => {

            if (data.ok)
                this.fileStore.dispatch(new RestoreFile(data.item));
            else
                console.error(data.errors[0].message);

        });
    }

    public DeleteFileForeverById(id: number) {

        this.httpClientHelper.Delete<FileModel>(this.BIN_DELETE_FILE_FOREVER_BY_ID_API + id).subscribe(data => {

            if (data.ok)
                this.fileStore.dispatch(new DeleteFileForever(data.item));
            else
                console.error(data.errors[0].message);
        });
    }

    public RestoreFolderById(id: number) {

        this.httpClientHelper.Get<FileModel>(this.BIN_RESTORE_FOLDER_BY_ID_API + id).subscribe(data => {

            if (data.ok)
                this.folderStore.dispatch(new RestoreFolder(data.item));
            else
                console.error(data.errors[0].message);
        });
    }

    public DeleteFolderForeverById(id: number) {

        this.httpClientHelper.Delete<FileModel>(this.BIN_DELETE_FOLDER_FOREVER_BY_ID_API + id).subscribe(data => {

            if (data.ok)
                this.folderStore.dispatch(new DeleteFolderForever(data.item));
            else
                console.error(data.errors[0].message);
        });
    }

}