import {Directive, ElementRef} from '@angular/core';

@Directive({
  selector: 'input[d-input]'
})
export class DInputDirective {

  constructor(private el: ElementRef) {
    el.nativeElement.style.borderRadius = '0px';
  }

}
