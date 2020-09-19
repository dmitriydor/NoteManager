import {Injectable} from '@angular/core';
import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {EMPTY, Observable, throwError} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {Router} from '@angular/router';
import {AuthenticateService} from '../../services/authenticate.service';

@Injectable()
export class AccessTokenInterceptor implements HttpInterceptor {

  constructor(private router: Router, private authService: AuthenticateService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const accessToken = this.authService.getAccessToken();
    const newReq = request.clone({headers: request.headers.set('Authorization', `Bearer ${accessToken}`)});
    return next.handle(newReq).pipe(
      catchError(err => {
        if (err instanceof HttpErrorResponse) {
          if (err.status === 401) {
            this.router.navigate(['/login']);
            return EMPTY;
          }
        }
        return throwError(err);
      })
    );
  }
}
