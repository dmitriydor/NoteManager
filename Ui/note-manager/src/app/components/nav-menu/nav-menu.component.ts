import { Component, OnInit } from '@angular/core';
import {BehaviorSubject, Observable, Subject} from 'rxjs';
import {UserService} from '../../services/user.service';
import {UserResponse} from '../../models/user.response';

@Component({
  selector: 'nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.sass']
})
export class NavMenuComponent implements OnInit {

  expand$ = new BehaviorSubject<boolean>(false);
  user: UserResponse;

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.userService.get().subscribe(x => this.user = x);
  }

  expand() {
    this.expand$.next(!this.expand$.value);
  }
}
