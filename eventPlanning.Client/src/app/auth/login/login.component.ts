import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccessData } from 'src/app/shared/models/access-data.model';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  errors: Array<string>;

  constructor(
    private router: Router,
    private authService: AuthenticationService,
  ) {
    this.loginForm = new FormGroup({
      email: new FormControl('', [Validators.email, Validators.required]),
      password: new FormControl('', Validators.required),
    });
    this.errors = new Array<string>();
  }
  
  ngOnInit(): void {}

  login(): void {
    const model = this.loginForm.value;
    this.authService.login(model).subscribe({
      next: (data: AccessData) => {
        this.errors.length = 0;
        if (data.accessToken != null) {
          this.router.navigateByUrl('/');
        }
      },
      error: (err) => {
        this.errors.push(err.message);
      }
    });
  }
}

