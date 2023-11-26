import { Injectable } from '@angular/core';
import { AbstractControl } from '@angular/forms';

@Injectable({ providedIn: 'root' })
export class PasswordsValidator {
  public validate(control: AbstractControl): { [key: string]: boolean } | null {
    if (
      control &&
      control.get('password') &&
      control.get('passwordConfirm') &&
      control.get('password')!.value === control.get('passwordConfirm')!.value
    ) {
      return null;
    }
    return { notSame: true };
  }
}
