import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OKTA_AUTH,OktaAuthStateService } from '@okta/okta-angular';
import OktaAuth, { AuthState } from '@okta/okta-auth-js';
import { Observable, filter, map } from 'rxjs';

@Component({
  selector: 'app-callback',
  templateUrl: './callback.component.html',
  styleUrls: ['./callback.component.scss']
})
export class CallbackComponent implements OnInit {
  public isAuthenticated$!: Observable<boolean>;
  constructor(private _router: Router,private _oktaStateService: OktaAuthStateService ) { }

  ngOnInit(): void {
    console.log('Is Authenticated ...')
    this.isAuthenticated$! = this._oktaStateService.authState$.pipe(
      filter((s: AuthState) => !!s),     
      map((s: AuthState) => s.isAuthenticated ?? false)      
    );
    
    if(this.isAuthenticated$){
      var accessToken= this._oktaStateService.authState$.subscribe(cl =>{
        return cl.accessToken ;
      });
      console.log('Access token: ', accessToken);
    }
  }

}
