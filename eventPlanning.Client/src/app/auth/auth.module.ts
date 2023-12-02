import { NgModule } from '@angular/core';
import { LoginComponent } from './login/login.component';
import { AuthRoutingModule } from './auth-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { CommonModule } from '@angular/common';
import { PasswordsValidator } from './services/passwords-validators';
import { RegistrationContainerComponent } from './registration-container/registration-container.component';
import { UserTypeInfoComponent } from './user-type-info/user-type-info.component';
import { UserInfoComponent } from './user-info/user-info.component';
import { AdditionalInfoComponent } from './additional-info/additional-info.component';


@NgModule({
  declarations: [
    LoginComponent,
    RegistrationContainerComponent, 
    UserTypeInfoComponent,
    UserInfoComponent,
    AdditionalInfoComponent
  ],
  imports: [
    AuthRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatSelectModule,
    CommonModule
  ],
  providers: [PasswordsValidator]
})
export class AuthModule { }
