import { Component } from '@angular/core';

@Component({
    selector: 'about',
    templateUrl: './about.component.html',
    styleUrls: ['./about.component.less']
})

export class AboutComponent {

  title: string;
    
  constructor() {
    this.title = "About";
  }
}
