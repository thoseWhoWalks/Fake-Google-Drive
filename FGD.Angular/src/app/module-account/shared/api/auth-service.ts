import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'; 

import { JwtHelperService } from '@auth0/angular-jwt';
 
import { ApiResponseModel } from 'src/app/shared/model/respose/api-response.model';
import { AuthJWTResponseModel } from '../models/auth-jwt-response.model';
import { AccountLoginModel } from '../models/acccount-login.model';

const httpOptions = {
    headers: new HttpHeaders({ 
      'Access-Control-Allow-Origin':'*' 
    })
  };

@Injectable()
export class AuthService{

    public API :  string = 'http://fakegoogledrive.eastus.cloudapp.azure.com:5000/api';
    public AUTH_API : string = `${this.API}/authtorization`;
 
    apiResponse : ApiResponseModel<AuthJWTResponseModel>;

    constructor(
        private httpClient:HttpClient, 
        public jwtHelper:JwtHelperService
        ) { 
            
        }

    public IsAuthenticated():boolean{
    
        var token = localStorage.getItem("token"); 
  
        if(token === null)
            return false; 
 
        return !this.jwtHelper.isTokenExpired(token);
    }

    public LogIn(loginModel: AccountLoginModel){
         return this.httpClient.post<ApiResponseModel<AuthJWTResponseModel>>(this.AUTH_API, loginModel,httpOptions);
    }

    public LogOut(){
        localStorage.removeItem("token");
        localStorage.removeItem("userId");
    }

}