import { Error } from './error.model';

export class ApiResponseModel<T>{
    
    public item:T;

    public ok:boolean;

    public errors:Array<Error>;
}
 