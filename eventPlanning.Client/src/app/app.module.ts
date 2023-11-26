import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { AuthModule } from './auth/auth.module';
import {
  AUTH_SERVICE,
  PROTECTED_FALLBACK_PAGE_URI,
  PUBLIC_FALLBACK_PAGE_URI,
  AuthModule as NgxAuth,
} from 'ngx-auth';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthenticationService } from './auth/services/authentication.service';

export function factory(
  authenticationService: AuthenticationService
): AuthenticationService {
  return authenticationService;
}

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule,
    AppRoutingModule,
    AuthModule,
    NgxAuth,
    BrowserAnimationsModule,
  ],
  providers: [
    {
      provide: AUTH_SERVICE,
      deps: [AuthenticationService],
      useFactory: factory,
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
