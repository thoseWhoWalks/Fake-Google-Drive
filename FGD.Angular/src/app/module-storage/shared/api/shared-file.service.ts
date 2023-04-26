import { Injectable } from '@angular/core';

import { Store } from '@ngrx/store';
import { FileState } from 'src/app/redux/app.state';

import { FileModel } from "../models/file.model";

import { SharedFileModel } from '../models/shared-file.model';

import { HttpClientHelper } from 'src/app/shared/helper/http-client.helper';
import { FileApiActions } from 'src/app/redux/actions/file.action';

@Injectable()
export class SharedFileService {

    constructor(private httpClientHelper: HttpClientHelper,
        private fileStore: Store<FileState>) {
    }

    SHARED_FILE_API: string = `SharedFile/`;

    public GetByUserId() {

        let ui = localStorage.getItem("userId");

        this.httpClientHelper.Get<FileModel[]>(this.SHARED_FILE_API + ui).subscribe(data => {

            if (data.ok)
                this.fileStore.dispatch(FileApiActions.load_files({ files: data.item }));
            else
                console.error(data.errors[0].message);
        });
    }

    public Create(model: SharedFileModel) {

        this.httpClientHelper.Post<SharedFileModel>(this.SHARED_FILE_API, model).subscribe(data => {

        });
    }

}