import { Injectable } from '@angular/core'; 
import { HttpClient } from '@angular/common/http';
import { ApiResponseModel } from 'src/app/shared/model/respose/api-response.model'; 
import { Observable } from 'rxjs';
import { HttpOptions } from './http-options';

const API_BASE_URL : string = "http://fakegoogledrive.eastus.cloudapp.azure.com:5000/api/";

@Injectable()
export class HttpClientHelper{

    defaultHttpOptions:HttpOptions;
  
    constructor(private http : HttpClient) {

        this.defaultHttpOptions = new HttpOptions();

     }
    
    public Get<T>(urlSegment:string,httpParams?:HttpOptions):Observable<ApiResponseModel<T>>{
 
        if(httpParams!==undefined)
            return this.http.get<ApiResponseModel<T>>(API_BASE_URL+urlSegment,httpParams.toObject());

       return this.http.get<ApiResponseModel<T>>(API_BASE_URL+urlSegment,this.defaultHttpOptions.toObject());
    }

    public Post<T>(urlSegment:string,body:T|FormData,httpParams?:HttpOptions):Observable<ApiResponseModel<T>>{
 
        if(httpParams!==undefined)
            return this.http.post<ApiResponseModel<T>>(API_BASE_URL+urlSegment,body,httpParams.toObject());

        return this.http.post<ApiResponseModel<T>>(API_BASE_URL+urlSegment,body,this.defaultHttpOptions.toObject());
    }

    public Put<T>(urlSegment:string,body?:T,httpParams?:HttpOptions){
  
        if(httpParams!==undefined)
        this.http.put<ApiResponseModel<T>>(API_BASE_URL+urlSegment,body,httpParams.toObject())

        return this.http.put<ApiResponseModel<T>>(API_BASE_URL+urlSegment,body,this.defaultHttpOptions.toObject());   
    }

     public Delete<T>(urlSegment:string,httpParams?:HttpOptions){

        if(httpParams!==undefined)
            return this.http.delete<ApiResponseModel<T>>(API_BASE_URL+urlSegment,httpParams.toObject());

        return this.http.delete<ApiResponseModel<T>>(API_BASE_URL+urlSegment,this.defaultHttpOptions.toObject());
    }

}

