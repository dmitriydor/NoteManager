import {AccessTokenInterceptor} from './access-token.interceptor';
import {HTTP_INTERCEPTORS} from '@angular/common/http';


export const httpInterceptorProviders = [
  { provide: HTTP_INTERCEPTORS, useClass: AccessTokenInterceptor, multi: true },
];
