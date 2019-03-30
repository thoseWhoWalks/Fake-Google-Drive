import { Component, OnInit, OnDestroy } from '@angular/core';
import {FormControl,FormGroup, Validators, NgForm} from '@angular/forms';

import { Subscription} from 'rxjs';
import { Store } from '@ngrx/store';
import { AuthState } from 'src/app/redux/app.state'; 

import { Router } from '@angular/router'; 

import { AuthService } from '../shared/api/auth-service';
import { AccountLoginModel } from '../shared/models/acccount-login.model'; 
import { LogIn } from 'src/app/redux/actions/auth.action';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit, OnDestroy {
 
  public myForm: FormGroup;

  logInModel:AccountLoginModel = new AccountLoginModel();

  serverError: string  = "";

  emailFormControl = new FormControl('', [
    Validators.required,
    Validators.email,
  ]);
 
  passwordFormControl = new FormControl('', [Validators.required]) ;

  hide : boolean = true;
 
  constructor(private authService : AuthService,
              private store : Store<AuthState>,
              private router: Router,
              private authStore : Store<AuthState>
              ) {  
              }
   
  private subscription : Subscription;

  ngOnInit() {
  
    this.myForm = new FormGroup({
      email: this.emailFormControl,
      password :  this.passwordFormControl
      }) 

      this.subscription = this.store.select("authPage").subscribe( res => {  
       console.log(res);
       
          if(localStorage.getItem("token")!="")
            this.router.navigateByUrl('/storage');
            
        }
        , error => {
           console.error(error);
           alert(error);
          }
      )

  }
 
  public onSignInClicked(value){

      this.logInModel  = value.value; 

      if(value.invalid)
        return;

      this.authService.LogIn(this.logInModel).subscribe(data => {

        console.log(data);
        

        if(data.ok) 
            this.authStore.dispatch(new LogIn(data.item));
        else{
          console.error(data.errors[0].message);
          this.serverError = data.errors[0].message;  
        }
        
      });;
      
  }
 
  ngOnDestroy(): void {
     this.subscription.unsubscribe();
  }
 
}

