import { Component, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { OKTA_AUTH, OktaAuthStateService } from '@okta/okta-angular';
import { AuthState, OktaAuth, Tokens } from '@okta/okta-auth-js';
//import { AuthState, OktaAuth, Tokens } from 'angular';
import { Observable, filter, map } from 'rxjs';

const DEFAULT_ORIGINAL_URI = window.location.origin;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'TodoApp';
   signIn1: any;
  
 // title = 'okta-angular-quickstart';
  public isAuthenticated$!: Observable<boolean>;

  constructor(private _router: Router, 
    private _oktaStateService: OktaAuthStateService, 
     @Inject(OKTA_AUTH) private _oktaAuth: OktaAuth) { }

  public ngOnInit(): void {
    console.log('Is Authenticated ...')
    this.isAuthenticated$! = this._oktaStateService.authState$.pipe(
      filter((s: AuthState) => !!s),     
      map((s: AuthState) => s.isAuthenticated ?? false)      
    );

    try {
     this._oktaAuth.handleLoginRedirect();
    //  if(this._oktaAuth.isAuthenticated()== true){
    //   const accessToken = this._oktaAuth.getAccessToken();     
    //   console.log('Access token:', accessToken);
    //  }
    } catch (error) {
      console.error('Error during callback:', error);
    }

    //console.log('Access token:', this._oktaAuth.getAccessToken());
    //this.SampleApp();
    // this.isAuthenticated$ = this._oktaStateService.authState$.pipe(
    //   map((authState: AuthState) => authState.isAuthenticated ?? false)
    // );
  }

  

  public async signIn() : Promise<void> {
    await this._oktaAuth.signInWithRedirect();
  }

  public async signOut(): Promise<void> {
    console.log('Signing out...')
    await this._oktaAuth.signOut();
    this._router.navigateByUrl('/');
  }

SampleApp(){
  // Handle the callback from Okta
  this._oktaAuth.handleLoginRedirect().then(() => {
    // Token has been obtained successfully
    const accessToken = this._oktaAuth.getAccessToken();
    console.log('Access token:', accessToken);
  })
}

}
