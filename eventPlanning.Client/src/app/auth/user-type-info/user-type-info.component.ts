import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { MyErrorStateMatcher } from '../services/my-error-state-matcher';
import { Role } from 'src/app/shared/models/roles.enum';

@Component({
  selector: 'app-user-type-info',
  templateUrl: './user-type-info.component.html',
  styleUrls: ['./user-type-info.component.css']
})
export class UserTypeInfoComponent implements OnInit {
  @Output() subformInitialized: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
  @Output() changeStep: EventEmitter<boolean> = new EventEmitter<boolean>();
  public userTypeInfoForm!: FormGroup;
  matcher!: ErrorStateMatcher;
  userRole!: Role;
  roles = Role;
  enumKeys!: number[];

constructor(private _fb: FormBuilder) {}

ngOnInit() {
  this.matcher = new MyErrorStateMatcher();
  this.enumKeys = Object.keys(this.roles).filter((f) => !isNaN(Number(f))).map(value => Number(value));
  this.userTypeInfoForm = this._fb.group({
    userType: new FormControl('', [Validators.required]),
});
  this.subformInitialized.emit(this.userTypeInfoForm);
  }

doChangeStep() {
  if (!this.userTypeInfoForm.valid) {
    this.userTypeInfoForm.markAsDirty();
    return;
  }
    this.changeStep.emit();
  }
}
