import { Component, OnInit } from '@angular/core';
import {BehaviorSubject} from 'rxjs';

@Component({
  selector: 'nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.sass']
})
export class NavMenuComponent implements OnInit {

  expand$ = new BehaviorSubject<boolean>(false);
  constructor() { }

  ngOnInit(): void {
  }

  expand() {
    this.expand$.next(!this.expand$.value);
  }

}
