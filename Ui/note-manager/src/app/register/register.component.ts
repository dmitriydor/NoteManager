import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {AuthenticateService} from '../services/authenticate.service';
import {AuthResponse} from '../models/auth.response';
import {tap} from 'rxjs/operators';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.sass']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;
  constructor(private fb: FormBuilder, private authenticateService: AuthenticateService) {
    this.registerForm = this.fb.group({
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  ngOnInit(): void {
  }

  register() {
    this.authenticateService.register(this.registerForm.value)
      .subscribe(response => {
        sessionStorage.setItem('access_token', response.token);
        sessionStorage.setItem('refresh_token', response.refreshToken);
      });
  }
}
