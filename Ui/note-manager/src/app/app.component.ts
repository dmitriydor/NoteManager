import { Component } from '@angular/core';
import {Router} from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})
export class AppComponent {
  title = 'note-manager';

  public constructor(private router: Router) {
  }

  visible(): boolean {
    switch (this.router.url) {
      case '/login':
      case '/register':
      case '/hello': return false;
    }
    return true;
  }
}
