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

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    request.clone({headers: request.headers.set('Authorizatin', `Bearer ${getToken}`)});
    return next.handle(request);
  }
}
