import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoginModel } from '../models/login.model';
import { RegistrationModel } from '../models/registration.model';
import { Observable } from 'rxjs';
import { AuthResponse } from '../models/auth.response';
import { environment } from '../../environments/environment';
import {tap} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthenticateService {

  constructor(private httpClient: HttpClient) { }
  private authenticatePath = `${environment.apiUrl}/authenticate`;
  private loginPath = `${this.authenticatePath}/login`;
  private registerPath = `${this.authenticatePath}/register`;
  private refreshTokenPath = `${this.authenticatePath}/refresh-token`;

  login(data: LoginModel): Observable<AuthResponse> {
    return this.httpClient.post<AuthResponse>(this.loginPath, data)
      .pipe(
        tap(response => {
          if (response.isAuthenticated) {
            sessionStorage.setItem('access_token', response.accessToken);
          }
        })
      );
  }

  register(data: RegistrationModel): Observable<AuthResponse> {
    return this.httpClient.post<AuthResponse>(this.registerPath, data).pipe(
      tap(response => {
        if (response.isAuthenticated) {
          sessionStorage.setItem('access_token', response.accessToken);
        }
      })
    );
  }

  refreshToken(): Observable<AuthResponse> {
    return this.httpClient.post<AuthResponse>(this.refreshTokenPath, null).pipe(
      tap(response => {
        if (response.isAuthenticated) {
          sessionStorage.setItem('access_token', response.accessToken);
        }
      })
    );
  }

  getAccessToken(): string {
    return sessionStorage.getItem('access_token');
  }

  isAuthenticated(): boolean {
    const accessToken = sessionStorage.getItem('access_token');
    return !!accessToken;
  }
}
