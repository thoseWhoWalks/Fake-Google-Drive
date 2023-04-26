import { Injectable } from '@angular/core';

import { FileModel } from "../models/file.model";

import { Store } from '@ngrx/store';
import { FileState, FolderState } from 'src/app/redux/app.state';

import { HttpClientHelper } from 'src/app/shared/helper/http-client.helper';
import { FolderPageAction } from 'src/app/redux/actions/folder.action';
import { FolderModel } from '../models/folder.model';
import { FilePageAction } from 'src/app/redux/actions/file.action';

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
                this.fileStore.dispatch(FilePageAction.restore_file(data.item));
            else
                console.error(data.errors[0].message);

        });
    }

    public DeleteFileForeverById(id: number) {

        this.httpClientHelper.Delete<FileModel>(this.BIN_DELETE_FILE_FOREVER_BY_ID_API + id).subscribe(data => {

            if (data.ok)
                this.fileStore.dispatch(FilePageAction.delete_file_forever(data.item));
            else
                console.error(data.errors[0].message);
        });
    }

    public RestoreFolderById(id: number) {

        this.httpClientHelper.Get<FolderModel>(this.BIN_RESTORE_FOLDER_BY_ID_API + id).subscribe(data => {

            if (data.ok)
                this.folderStore.dispatch(FolderPageAction.restore_folder(data.item));
            else
                console.error(data.errors[0].message);
        });
    }

    public DeleteFolderForeverById(id: number) {

        this.httpClientHelper.Delete<FolderModel>(this.BIN_DELETE_FOLDER_FOREVER_BY_ID_API + id).subscribe(data => {

            if (data.ok)
                this.folderStore.dispatch(FolderPageAction.delete_folder_forever(data.item));
            else
                console.error(data.errors[0].message);
        });
    }

}