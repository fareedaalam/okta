import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { OktaAuthModule, OKTA_CONFIG } from '@okta/okta-angular';
import { OktaAuth } from '@okta/okta-auth-js';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProfileComponent } from './components/profile/profile.component';

const oktaAuth = new OktaAuth({
  issuer: 'https://dev-46138610.okta.com',
  clientId: '0oa9jsz5b8rk9zdXI5d7',  
  redirectUri: 'http://localhost:4200/login/callback',
  scopes: ['openid', 'profile', 'email'],
  pkce: true
});


@NgModule({
  declarations: [
    AppComponent,
    ProfileComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    OktaAuthModule.forRoot({oktaAuth})
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
