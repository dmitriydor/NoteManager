import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { AuthenticateService } from '../services/authenticate.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthenticateService) { }

  canActivate() {
    return this.authService.isAuthenticated();
  }
}
