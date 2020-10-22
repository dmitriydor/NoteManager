import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'd-page',
  templateUrl: './page.component.html',
  styleUrls: ['./page.component.sass']
})
export class PageComponent implements OnInit {

  title: string;
  constructor() { }

  ngOnInit(): void {
    // todo: определение заголовка из маршрута.
  }

}
