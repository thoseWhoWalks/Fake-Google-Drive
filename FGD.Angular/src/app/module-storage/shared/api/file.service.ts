import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';

import { FileModel } from "../models/file.model";

import { Store } from '@ngrx/store';
import { FileState } from 'src/app/redux/app.state';

import { saveAs } from 'file-saver';
import { HttpClientHelper } from 'src/app/shared/helper/http-client.helper';
import { HttpOptions } from "src/app/shared/helper/http-options";
import { FileApiActions, FilePageAction } from 'src/app/redux/actions/file.action';

@Injectable()
export class FileService {

  userId: number = 0;

  constructor(private httpClientHelper: HttpClientHelper,
    private fileStore: Store<FileState>) {
    this.userId = Number.parseInt(localStorage.getItem("userId"));
  }

  STORED_FILE_API: string = `StoredFile/`;

  FILE_GETBYROOT_API: string = `StoredFile/GetByRootByUserId/`;
  FILE_GET_BY_PARENT_FOLDER_ID: string = `StoredFile/GetByStoredFolderId/`;

  FILE_GET_DELETED: string = `${this.STORED_FILE_API}GetDeleted/`

  FILE_DOWNLOAD_API: string = `${this.STORED_FILE_API}Download/`;
  FILE_UPLOAD_API: string = `${this.STORED_FILE_API}`;

  public GetRootByUserId() {

    this.httpClientHelper.Get<FileModel[]>(this.FILE_GETBYROOT_API + this.userId).subscribe(data => {

      if (data.ok)
        this.fileStore.dispatch(FileApiActions.load_files({ files: data.item }));
      else
        console.error(data.errors[0].message);
    });
  }

  public Update(file: FileModel) {

    if (file.title === undefined)
      return
    if (file.sizeInKbs === 0)
      return

    this.httpClientHelper.Put<FileModel>(
      this.STORED_FILE_API + file.id, file).subscribe(data => {

        if (data.ok)
          this.fileStore.dispatch(FilePageAction.update_file(data.item));
        else
          console.error(data.errors[0].message);
      }
      );

  }

  public GetByParentFolderId(id: number) {

    this.httpClientHelper.Get<FileModel[]>(this.FILE_GET_BY_PARENT_FOLDER_ID + id).subscribe(data => {

      if (data.ok)
        this.fileStore.dispatch(FileApiActions.load_files({ files: data.item }));
      else
        console.error(data.errors[0].message);
    });
  }

  public GetDeletedByUserId() {

    this.httpClientHelper.Get<FileModel[]>(this.FILE_GET_DELETED + this.userId).subscribe(data => {

      if (data.ok)
        this.fileStore.dispatch(FileApiActions.load_files({ files: data.item }));
      else
        console.error(data.errors[0].message);
    });
  }

  public Download(file: FileModel) {

    this.httpClientHelper.Get(this.FILE_DOWNLOAD_API + file.id,new HttpOptions(
      {
        responseType: 'blob',
        headers: new HttpHeaders()
          .append('Content-Type', 'application/json')
          .append('Access-Control-Allow-Origin', '*')
          .append('Authorization', 'Bearer ' + localStorage.getItem("token"))
      }))
      .subscribe(data => {
        saveAs(data, file.title + file.extention);
      });

  }

  public DeleteById(id: any) {

    this.httpClientHelper.Delete<FileModel>(this.STORED_FILE_API + id).subscribe(data => {

      if (data.ok)
        this.fileStore.dispatch(FilePageAction.delete_file(data.item));
      else
        console.error(data.errors[0].message);
    });
  }

  public Upload(file: FileModel) {

    const formData: FormData = new FormData();

    formData.append("file", file.file);

    if (file.StoredFolderId != null)
      formData.append("StoredFolderId", file.StoredFolderId.toString());

    this.httpClientHelper.Post<FileModel[]>(
      this.FILE_UPLOAD_API + this.userId, formData).subscribe(data => {

        if (data.ok)
          this.fileStore.dispatch(FilePageAction.add_file({ files: data.item }));
        else
          console.error(data.errors[0].message);
      }
      );

  }

}