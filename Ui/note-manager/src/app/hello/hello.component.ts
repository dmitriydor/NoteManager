import { Component, OnInit } from '@angular/core';
import {HomeService} from '../services/home.service';
import {Observable} from 'rxjs';

@Component({
  selector: 'app-hello',
  templateUrl: './hello.component.html',
  styleUrls: ['./hello.component.sass']
})
export class HelloComponent implements OnInit {

  loaded$: Observable<boolean>;
  constructor(private homeService: HomeService) {
    this.loaded$ = this.homeService.get();
  }

  ngOnInit(): void {
  }

}
