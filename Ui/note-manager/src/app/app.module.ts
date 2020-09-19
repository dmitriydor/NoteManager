import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { DButtonDirective } from './directives/d-button.directive';
import { DInputDirective } from './directives/d-input.directive';
import { ReactiveFormsModule } from '@angular/forms';
import { RegisterComponent } from './components/register/register.component';
import { AuthenticateService } from './services/authenticate.service';
import { HttpClientModule } from '@angular/common/http';
import { HelloComponent } from './components/hello/hello.component';
import { httpInterceptorProviders } from './infrastructure/http-interceptors/providers';

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
  providers: [AuthenticateService, httpInterceptorProviders],
  bootstrap: [AppComponent]
})
export class AppModule { }
