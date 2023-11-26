import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { MyErrorStateMatcher } from '../services/my-error-state-matcher';

@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.css']
})
export class UserInfoComponent implements OnInit {
  @Output() subformInitialized: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
  @Output() changeStep: EventEmitter<boolean> = new EventEmitter<boolean>();
  public userInfoForm!: FormGroup;
  matcher!: ErrorStateMatcher;

  constructor(private _fb: FormBuilder) { }

  ngOnInit() {
    this.matcher = new MyErrorStateMatcher();
    this.userInfoForm = this._fb.group({
      email: new FormControl('', [Validators.email, Validators.required]),
      userName: new FormControl('', [Validators.required]),
      phoneNumber: new FormControl(),
      password: new FormControl('', Validators.required),
      passwordConfirm: new FormControl('', Validators.required),
    });
    this.subformInitialized.emit(this.userInfoForm);
  }

  doChangeStep() {
    if (!this.userInfoForm.valid) {
      this.userInfoForm.markAsDirty();
      return;
    }
    this.changeStep.emit();
  }
}
