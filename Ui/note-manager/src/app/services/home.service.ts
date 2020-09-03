import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class HomeService {
  constructor(private httpClient: HttpClient) { }
  private homePath = environment.apiUrl;
  get(): Observable<boolean> {
    return  this.httpClient.get<boolean>(this.homePath);
  }
}
