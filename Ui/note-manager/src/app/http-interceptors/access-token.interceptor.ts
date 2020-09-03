import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { getToken } from '../utils/access.token';

@Injectable()
export class AccessTokenInterceptor implements HttpInterceptor {

  constructor() {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const newReq = request.clone({headers: request.headers.set('Authorization', `Bearer ${getToken()}`)});
    return next.handle(newReq);
  }
}
