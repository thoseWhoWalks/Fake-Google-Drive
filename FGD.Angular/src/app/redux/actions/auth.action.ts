import { Action } from '@ngrx/store';
import { AuthJWTResponseModel } from 'src/app/module-account/shared/models/auth-jwt-response.model';

export namespace AUTH_ACTION{
    
    export const LOG_OUT = "LOG_OUT";
    
    export const LOG_IN = "LOG_IN";    

}

export class LogIn implements Action{
  
   readonly type: string = AUTH_ACTION.LOG_IN;

   constructor(public payload:AuthJWTResponseModel) {
       
   }

}

export class LogOut implements Action{
  
    readonly type: string = AUTH_ACTION.LOG_OUT;
 
    constructor(public payload = null) {
        
    }
 
 }

 export type AuthAction =  LogOut | LogIn
