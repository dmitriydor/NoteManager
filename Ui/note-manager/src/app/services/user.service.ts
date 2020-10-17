import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import {UserResponse} from '../models/user.response';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient: HttpClient) { }
  private userPath = `${environment.apiUrl}/user`;

  get(): Observable<UserResponse> {
    return this.httpClient.get<UserResponse>(this.userPath);
  }
}
