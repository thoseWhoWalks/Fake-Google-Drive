import { Injectable } from '@angular/core';

import { HttpClient, HttpHeaders } from '@angular/common/http';

import { ApiResponseModel } from 'src/app/shared/model/respose/api-response.model';
import { AccountSignUpModel } from '../models/account-sign-up-model';
import { AccountModel } from '../models/account.model';

const httpOptions = {
  headers: new HttpHeaders({ 
    'Access-Control-Allow-Origin':'*' 
  })
};

@Injectable()
export class AccountService{
 
  static BaseUrl : string = "http://fakegoogledrive.eastus.cloudapp.azure.com:5000/api/account";
 
  constructor(private http : HttpClient) { 
   }

  public SignUp(model:AccountSignUpModel) {
      
      return this.http.post<ApiResponseModel<AccountModel>>(AccountService.BaseUrl,model,httpOptions);

  }

}