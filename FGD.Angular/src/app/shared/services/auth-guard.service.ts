import { CanActivate } from '@angular/router';
import { AuthService } from 'src/app/module-account/shared/api/auth-service';
import { Router } from '@angular/router';
import { Injectable } from '@angular/core';

@Injectable()
export class AuthGuardService implements CanActivate{
     
    constructor(public auth:AuthService, public router: Router){

    }

    canActivate():boolean{
        if(!this.auth.IsAuthenticated()){
            this.router.navigate(['signin']);
                return false;
        }

        return true;
    }
    

} 