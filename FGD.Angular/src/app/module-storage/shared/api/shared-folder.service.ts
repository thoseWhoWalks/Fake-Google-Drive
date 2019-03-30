import { Injectable } from '@angular/core';

import { Store } from '@ngrx/store';
import { FolderState } from 'src/app/redux/app.state';
import { LoadFolders } from 'src/app/redux/actions/folder.action';

import { SharedFolderModel } from '../models/shared-folder.model';
import { FolderModel } from '../models/folder.model';

import { HttpClientHelper } from 'src/app/shared/helper/http-client.helper';

@Injectable()
export class SharedFolderService {

    constructor(
        private httpClientHelper: HttpClientHelper,
        private folderStore: Store<FolderState>
    ) {
    }

    SHARED_FOLDER_API: string = `SharedFolder/`;

    public GetByUserId() {

        let ui = localStorage.getItem("userId");

        this.httpClientHelper.Get<FolderModel[]>(this.SHARED_FOLDER_API + ui).subscribe(data => {

            if (data.ok)
                this.folderStore.dispatch(new LoadFolders(data.item));
            else
                console.error(data.errors[0].message);
        });
    }

    public Create(model: SharedFolderModel) {

        this.httpClientHelper.Post<SharedFolderModel>(this.SHARED_FOLDER_API, model).subscribe(data => {

        });
    }

}