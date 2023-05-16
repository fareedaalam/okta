import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OktaCallbackComponent } from '@okta/okta-angular';
import { ProfileComponent } from './components/profile/profile.component';
import { LoginComponent } from './login/login.component';
import { CallbackComponent } from './callback/callback.component';


const routes: Routes = [
  { path: 'login/callback', component: CallbackComponent },
  {path: 'profile',component:ProfileComponent},
  {path: 'login',component:LoginComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
