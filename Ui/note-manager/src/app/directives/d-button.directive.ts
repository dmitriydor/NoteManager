import {Directive, ElementRef} from '@angular/core';

@Directive({
  selector: 'button[d-button]'
})
export class DButtonDirective {

  constructor(element: ElementRef) {
    element.nativeElement.style.paddingLeft = '20px';
    element.nativeElement.style.paddingRight = '20px';
    element.nativeElement.style.paddingTop = '12px';
    element.nativeElement.style.paddingBottom = '12px';
    element.nativeElement.style.border = 'none';
    element.nativeElement.style.borderRadius = '0px';
  }

}
