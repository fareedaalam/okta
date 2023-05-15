import { Component, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { OKTA_AUTH, OktaAuthStateService } from '@okta/okta-angular';
import { AuthState, OktaAuth } from '@okta/okta-auth-js';
import { Observable, filter, map } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'TodoApp';
 // title = 'okta-angular-quickstart';
  public isAuthenticated$!: Observable<boolean>;

  constructor(private _router: Router, private _oktaStateService: OktaAuthStateService, @Inject(OKTA_AUTH) private _oktaAuth: OktaAuth) { }

  public ngOnInit(): void {
    this.isAuthenticated$! = this._oktaStateService.authState$.pipe(
      filter((s: AuthState) => !!s),
     
      map((s: AuthState) => s.isAuthenticated ?? false)
    );
    // this.isAuthenticated$ = this._oktaStateService.authState$.pipe(
    //   map((authState: AuthState) => authState.isAuthenticated ?? false)
    // );
  }

  public async signIn() : Promise<void> {
    await this._oktaAuth.signInWithRedirect();
  }

  public async signOut(): Promise<void> {
    await this._oktaAuth.signOut();
   // this.router.navigateByUrl('/');
  }
}
