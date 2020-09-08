import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoginModel } from '../models/login.model';
import { RegistrationModel } from '../models/registration.model';
import { Observable } from 'rxjs';
import { AuthResponse } from '../models/auth.response';
import { environment } from '../../environments/environment';

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
    return this.httpClient.post<AuthResponse>(this.loginPath, data);
  }

  register(data: RegistrationModel): Observable<AuthResponse> {
    return this.httpClient.post<AuthResponse>(this.registerPath, data);
  }

  refreshToken(): Observable<AuthResponse> {
    return this.httpClient.post<AuthResponse>(this.refreshTokenPath, null);
  }
}
