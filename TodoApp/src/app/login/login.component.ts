import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { OKTA_AUTH, OktaAuthStateService } from '@okta/okta-angular';
import OktaAuth from '@okta/okta-auth-js';
import { CompareService } from '../compare.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup ;

  constructor(private fb: FormBuilder,private _router: Router, 
    private _oktaStateService: OktaAuthStateService, 
     @Inject(OKTA_AUTH) private _oktaAuth: OktaAuth,
     private _compare:CompareService) { }

  ngOnInit() {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }

  login() {
    if (this.loginForm.invalid) {
      return;
    }

    // Implement your login logic here
    const email = this.loginForm.value.email;
    const password = this.loginForm.value.password;

    // Example login logic:
    console.log('Email:', email);
    console.log('Password:', password);

    // Reset the form after login
    this.loginForm.reset();
  }

  externalLogin(provider: string) {
   
    // Implement external login logic here based on the provider (e.g., Facebook, Google)
    // Handle the callback from Okta

    //if(provider === 'okta'){
    
  this._oktaAuth.handleLoginRedirect().then(() => {
    // Token has been obtained successfully
    const accessToken = this._oktaAuth.getAccessToken();
    console.log('Access token:', accessToken);
  })
//}
  }
}
