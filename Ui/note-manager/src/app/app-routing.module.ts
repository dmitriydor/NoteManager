import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { HelloComponent } from './components/hello/hello.component';
import { AuthGuard } from './infrastructure/guards/AuthGuard';
import {NotesComponent} from './components/notes/notes.component';

const routes: Routes = [
  {path: '', pathMatch: 'full', redirectTo: 'hello'},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'hello', component: HelloComponent},
  {path: 'notes', component: NotesComponent, canActivate: [AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [AuthGuard]
})
export class AppRoutingModule { }
