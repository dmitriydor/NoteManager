import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { DButtonDirective } from './d-button.directive';
import { DInputDirective } from './d-input.directive';
import { ReactiveFormsModule } from '@angular/forms';
import { RegisterComponent } from './register/register.component';
import { AuthenticateService } from './services/authenticate.service';
import { HttpClientModule } from '@angular/common/http';
import { HelloComponent } from './hello/hello.component';
import { httpInterceptorProviders } from './http-interceptors/providers';
import { HomeService } from './services/home.service';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    DButtonDirective,
    DInputDirective,
    RegisterComponent,
    HelloComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
  ],
  providers: [AuthenticateService, HomeService, httpInterceptorProviders],
  bootstrap: [AppComponent]
})
export class AppModule { }
