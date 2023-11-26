import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Role } from 'src/app/shared/models/roles.enum';

@Component({
  selector: 'app-additional-info',
  templateUrl: './additional-info.component.html',
  styleUrls: ['./additional-info.component.css']
})
export class AdditionalInfoComponent {
  @Input() userRole!: Role;
  @Output() subformInitialized: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
  @Output() changeStep: EventEmitter<boolean> = new EventEmitter<boolean>();
  public additionalInfoForm!: FormGroup;
  public roles = Role;

  constructor(private _fb: FormBuilder) { }

  ngOnInit() {
    console.log(this.userRole);
    if (this.userRole == this.roles.Creator) {
      this.additionalInfoForm = this._fb.group({
        organizationName: new FormControl('', [Validators.required]),
        website: new FormControl('', [Validators.required])
      });
    } else if (this.userRole == this.roles.Guest) {
      this.additionalInfoForm = this._fb.group({
        birthDate: new FormControl('', [Validators.required]),
      });
    }
    this.subformInitialized.emit(this.additionalInfoForm);
  }

  doChangeStep() {
    if (!this.additionalInfoForm.valid) {
      this.additionalInfoForm.markAsDirty();
      return;
    }
    this.changeStep.emit();
  }
}
