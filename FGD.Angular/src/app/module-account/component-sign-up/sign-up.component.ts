import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormControl, Validators, FormBuilder, FormGroup, NgForm } from '@angular/forms';

import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

import { AccountService } from '../shared/api/account-service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit, OnDestroy {

  constructor(private accountService: AccountService,
    private formBuilder: FormBuilder,
    private router: Router
  ) { }

  signUpForm: FormGroup;

  serverError: string = null;

  hide: boolean = true;

  emailFormControl: FormControl;
  passwordFormControl: FormControl;
  alphabeticFormControl: FormControl;
  ageFormControl: FormControl;

  sunscription: Subscription = null;

  ngOnInit() {

    this.emailFormControl = new FormControl('', [
      Validators.required,
      Validators.email,
    ]);

    this.passwordFormControl = new FormControl('', [
      Validators.required
    ])

    this.alphabeticFormControl = new FormControl('', [
      Validators.pattern('^[a-zA-Z ]*$')
    ]);

    this.ageFormControl = new FormControl("",[
      Validators.required
    ])

    this.signUpForm = this.formBuilder.group({
      email: this.emailFormControl,
      firstName: this.alphabeticFormControl,
      lastName: this.alphabeticFormControl,
      age : this.ageFormControl,
      password: this.passwordFormControl,
      passwordConfirm: this.passwordFormControl
    });

  }

  onSignUpClicked(value) {

    let model = value.value;

    this.sunscription = this.accountService.SignUp(model).subscribe(res => {

      if (res.ok)
        this.router.navigateByUrl('/signin');
      else {
        console.error(res.errors[0]);
        this.serverError = res.errors[0].message;
      }
    },
      error => {
        console.error(error);
      }
    );
  }

  ngOnDestroy(): void {
    if (this.sunscription !== null)
      this.sunscription.unsubscribe();
  }


}




