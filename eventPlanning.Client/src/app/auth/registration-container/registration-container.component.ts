import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { BehaviorSubject, Observable } from 'rxjs';
import { AccessData } from 'src/app/shared/models/access-data.model';
import { RegisterModel } from 'src/app/shared/models/register.model';
import { Role } from 'src/app/shared/models/roles.enum';
import { AuthenticationService } from '../services/authentication.service';
import { Router } from '@angular/router';

type Step = 'userInfo' | 'userTypeInfo' | 'additionalInfo';

@Component({
  selector: 'app-registration-container',
  templateUrl: './registration-container.component.html',
  styleUrls: ['./registration-container.component.css']
})
export class RegistrationContainerComponent implements OnInit {
  private currentStepBs: BehaviorSubject<Step> = new BehaviorSubject<Step>('userInfo');
  public currentStep$: Observable<Step> = this.currentStepBs.asObservable();
  public userForm!: FormGroup;
  public roles = Role;
  public errors: Array<string> = new Array<string>();


  constructor(
    private router: Router,
    private _fb: FormBuilder,
    private authService: AuthenticationService) {}
  ngOnInit() {
    this.userForm = this._fb.group({
      userInfo: null,
      userTypeInfo: null,
      additionalInfo: null
    });
  }

  subformInitialized(name: string, group: FormGroup) {
    this.userForm.setControl(name, group);
  }

  changeStep(currentStep: string) {
    switch(currentStep) {
      case 'userInfo':
        this.currentStepBs.next('userTypeInfo');
        break;
      case 'userTypeInfo':
        this.currentStepBs.next('additionalInfo');
        break;
      case 'additionalInfo':
        this.submitForm();
        break;
    }
  }

  submitForm() {
        const formData = this.userForm.value;

        const model: RegisterModel = {
          email: formData.userInfo.email,
          userName: formData.userInfo.userName,
          password: formData.userInfo.password,
          phoneNumber: formData.userInfo.phoneNumber,
          userType: formData.userTypeInfo.userType,
          birthDate: formData.additionalInfo.birthDate,
          organizationName: formData.additionalInfo.organizationName,
          website: formData.additionalInfo.website,
        };

        this.authService.registration(model).subscribe(
          (data: AccessData) => {
            if (data.accessToken != null) { 
              this.router.navigateByUrl('/');
            }
          },
          (err) => {
            this.errors = this.errors.concat(err.error);
          }
        );
  }

  getRole(): Role {
    return this.userForm.get('userTypeInfo')?.value.userType as Role;
  }
}
